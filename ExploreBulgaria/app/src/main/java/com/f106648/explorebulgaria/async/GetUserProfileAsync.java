package com.f106648.explorebulgaria.async;

import static android.content.Context.MODE_PRIVATE;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.JsonElement;
import com.google.gson.JsonParser;
import com.f106648.explorebulgaria.DatabaseHelper;
import com.f106648.explorebulgaria.LandmarksApiRetrofit;
import com.f106648.explorebulgaria.servicemodels.UserProfileResponse;

import java.io.IOException;
import java.util.concurrent.TimeUnit;

import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class GetUserProfileAsync extends AsyncTask<Void, Void, Void> {
    private Retrofit retrofit;
    private LandmarksApiRetrofit apiRetrofit;
    private final String baseUrl = "http://10.0.2.2:8080";
    private String token;
    private Context context;

    private DatabaseHelper dbHelper;
    private SharedPreferences sharedPreferences;
    private SharedPreferences.Editor sharedPreferencesEditor;

    public GetUserProfileAsync(Context context, String token) {
        super();
        this.token = token;
        this.context = context;
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();
    }
    //Системата извиква метода за да  изпълни задачата в работната нишка и му предоставя параметъра, включен в AsyncTask.execute()
    @Override
    protected Void doInBackground(Void... arg0) {

        OkHttpClient okHttpClient = new OkHttpClient.Builder().addInterceptor(new Interceptor() {
                    @Override
                    public okhttp3.Response intercept(Chain chain) throws IOException {
                        Request newRequest  = chain.request().newBuilder()
                                .addHeader("Authorization", "Bearer " + token)
                                .build();
                        return chain.proceed(newRequest);
                    }
                })
                .readTimeout(60, TimeUnit.SECONDS)
                .connectTimeout(60, TimeUnit.SECONDS)
                .build();

        retrofit = new Retrofit.Builder()
                .baseUrl(baseUrl)
                .addConverterFactory(GsonConverterFactory.create())
                .client(okHttpClient)
                .build();

        apiRetrofit = retrofit.create(LandmarksApiRetrofit.class);

        Call<ResponseBody> call = apiRetrofit.GetUserProfile();
        call.enqueue(new Callback<ResponseBody>() {
            @Override
            public void onResponse(Call<ResponseBody> call, Response<ResponseBody> response) {
                if (response.isSuccessful()) {
                    try {
                        JsonParser parser = new JsonParser();
                        JsonElement mJson =  parser.parse(response.body().string());
                        Gson gson = new Gson();
                        UserProfileResponse object = gson.fromJson(mJson, UserProfileResponse.class);

                        dbHelper = new DatabaseHelper(context);
                        long id = dbHelper.addUser(object.getUserName(), object.getFirstName(), object.getLastName(), object.getEmail());
                        dbHelper.close();

                        sharedPreferences = context.getSharedPreferences("TokenPref", MODE_PRIVATE);
                        sharedPreferencesEditor = sharedPreferences.edit();
                        /* Store token */
                        sharedPreferencesEditor.putLong("userid", id);
                        /* Commit the changes (add token to the file) */
                        sharedPreferencesEditor.commit();

                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                } else {
                    int statusCode = response.code();
                    // handle the error here
                    Log.e("Error","Error with status code:"+statusCode);
                }
            }
            @Override
            public void onFailure(Call<ResponseBody> call, Throwable t) {
                // handle the error here
                Log.e("Error",t.getMessage());
            }
        });

        return null;
    }
    // Системата извиква този метод за да се изпълни в UI нишката и да достави резултата от метода doInBackground()
    @Override
    protected void onPostExecute(Void result) {
        super.onPostExecute(result);
    }
}
