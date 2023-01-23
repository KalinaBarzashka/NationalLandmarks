package com.f106648.explorebulgaria;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.Gson;
import com.google.gson.JsonElement;
import com.google.gson.JsonParser;
import com.f106648.explorebulgaria.servicemodels.LoginRequest;
import com.f106648.explorebulgaria.servicemodels.LoginResponse;

import java.io.IOException;
import java.util.concurrent.TimeUnit;

import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.RequestBody;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class MainActivity extends AppCompatActivity {

    private EditText uiLoginUsername;
    private EditText uiLoginPassword;

    private SharedPreferences.Editor sharedPreferencesEditor;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        uiLoginUsername = findViewById(R.id.loginUsername);
        uiLoginPassword = findViewById(R.id.loginPassword);
        //uiLoginBtn = findViewById(R.id.loginBtn);
        //uiAttemptsInfo = findViewById(R.id.attemptsInfo);
        //private Button uiLoginBtn;
        //private TextView uiAttemptsInfo;
        TextView uiGoToRegister = findViewById(R.id.goToRegister);

        SharedPreferences sharedPreferences = getApplicationContext().getSharedPreferences("TokenPref", MODE_PRIVATE);
        sharedPreferencesEditor = sharedPreferences.edit();

        uiGoToRegister.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this, RegisterActivity.class);
                startActivity(intent);
            }
        });
    }

    public void login(View view) {
        String inputUsername = uiLoginUsername.getText().toString();
        String inputPassword = uiLoginPassword.getText().toString();

        if(inputUsername.isEmpty() || inputPassword.isEmpty()) {
            Toast.makeText(MainActivity.this, "Please enter username and password!", Toast.LENGTH_SHORT).show();
            return;
        }

        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .readTimeout(60, TimeUnit.SECONDS)
                .connectTimeout(60, TimeUnit.SECONDS)
                .build();

        String baseUrl = "http://10.0.2.2:8080";
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(baseUrl)
                .addConverterFactory(GsonConverterFactory.create())
                .client(okHttpClient)
                .build();

        LandmarksApiRetrofit apiRetrofit = retrofit.create(LandmarksApiRetrofit.class);

        LoginRequest request = new LoginRequest(inputUsername, inputPassword);
        String json = new Gson().toJson(request);
        RequestBody requestBody = RequestBody.create(MediaType.parse("application/json"), json);

        Call<ResponseBody> call = apiRetrofit.Login(requestBody);
        call.enqueue(new Callback<ResponseBody>() {
            @Override
            public void onResponse(@NonNull Call<ResponseBody> call, @NonNull Response<ResponseBody> response) {
                if (response.isSuccessful()) {
                    try {
                        JsonParser parser = new JsonParser();
                        JsonElement mJson =  parser.parse(response.body().string());
                        Gson gson = new Gson();
                        LoginResponse object = gson.fromJson(mJson, LoginResponse.class);

                        /* Store token */
                        sharedPreferencesEditor.putString("token", object.getToken());
                        /* Commit the changes (add token to the file) */
                        sharedPreferencesEditor.commit();

                        String responseString = response.body().string();
                        // do something with the response string
                        Log.d("Response",responseString);
                        Intent intent = new Intent(MainActivity.this, HomePageActivity.class);
                        startActivity(intent);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                } else {
                    int statusCode = response.code();
                    if (response.code() == 403) {
                        try {
                            Toast.makeText(MainActivity.this, response.errorBody().string(), Toast.LENGTH_LONG).show(); //fix
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                    // handle the error here
                    Log.e("Error","Error with status code:"+statusCode);
                }
            }
            @Override
            public void onFailure(@NonNull Call<ResponseBody> call, @NonNull Throwable t) {
                // handle the error here
                Log.e("Error",t.getMessage());
            }
        });
    }
}