package com.infm308.explorebulgaria.async;

import android.os.AsyncTask;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.JsonElement;
import com.google.gson.JsonParser;
import com.infm308.explorebulgaria.HomePageActivity;
import com.infm308.explorebulgaria.LandmarkAdapter;
import com.infm308.explorebulgaria.LandmarksApiRetrofit;
import com.infm308.explorebulgaria.models.Landmark;
import com.infm308.explorebulgaria.servicemodels.LandmarksResponse;

import java.io.IOException;
import java.util.ArrayList;
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

public class GetLandmarksAsync extends AsyncTask<Void, Void, ArrayList<Landmark>> {
    private Retrofit retrofit;
    private LandmarksApiRetrofit apiRetrofit;
    private final String baseUrl = "http://10.0.2.2:8080";
    private String token;
    private ArrayList<Landmark> landmarks;
    private LandmarkAdapter landmarkAdapter;

    public GetLandmarksAsync(String token) {
        super();
        this.token = token;
        this.landmarks = new ArrayList<>();
    }

    @Override
    protected void onPreExecute() {
        landmarkAdapter = (LandmarkAdapter) HomePageActivity.recyclerView.getAdapter();
        super.onPreExecute();
    }
    //Системата извиква метода за да  изпълни задачата в работната нишка и му предоставя параметъра, включен в AsyncTask.execute()
    @Override
    protected ArrayList<Landmark> doInBackground(Void... arg0) {

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

        Call<ResponseBody> call = apiRetrofit.GetLandmarks();
        call.enqueue(new Callback<ResponseBody>() {
            @Override
            public void onResponse(Call<ResponseBody> call, Response<ResponseBody> response) {
                if (response.isSuccessful()) {
                    try {
                        JsonParser parser = new JsonParser();
                        JsonElement mJson =  parser.parse(response.body().string());
                        Gson gson = new Gson();
                        LandmarksResponse object = gson.fromJson(mJson, LandmarksResponse.class);

                        landmarks = object.getLandmarks();

                        //Landmark landmark = new Landmark();
                        //landmarks.add(landmark);

                        landmarkAdapter.setNewList(landmarks);
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

        return landmarks;
    }
// Системата извиква този метод за да се изпълни в UI нишката и да достави резултата от метода doInBackground()
    @Override
    protected void onPostExecute(ArrayList<Landmark> result) {
        landmarkAdapter.setNewList(result);
        super.onPostExecute(result);
    }
}
