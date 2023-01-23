package com.infm308.explorebulgaria;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.Gson;
import com.google.gson.JsonElement;
import com.google.gson.JsonParser;
import com.infm308.explorebulgaria.servicemodels.RegisterRequest;
import com.infm308.explorebulgaria.servicemodels.RegisterResponse;

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

public class RegisterActivity extends AppCompatActivity {

    private EditText uiRegisterFirstName;
    private EditText uiRegisterLastName;
    private EditText uiRegisterUsername;
    private EditText uiRegisterPassword;
    private EditText uiRegisterConfPassword;
    private EditText uiRegisterEmail;
    private Button uiRegisterBtn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        uiRegisterFirstName = findViewById(R.id.registerFirstName);
        uiRegisterLastName = findViewById(R.id.registerLastName);
        uiRegisterUsername = findViewById(R.id.registerUsername);
        uiRegisterPassword = findViewById(R.id.registerPassword);
        uiRegisterConfPassword = findViewById(R.id.registerConfPassword);
        uiRegisterEmail = findViewById(R.id.registerEmail);
        uiRegisterBtn = findViewById(R.id.registerBtn);
        TextView uiGoToLogin = findViewById(R.id.goToLogin);

        uiGoToLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(RegisterActivity.this, MainActivity.class);
                startActivity(intent);
            }
        });
    }

    public void register(View view) {
        String firstName = uiRegisterFirstName.getText().toString();
        String lastName = uiRegisterLastName.getText().toString();
        String username = uiRegisterUsername.getText().toString();
        String password = uiRegisterPassword.getText().toString();
        String confPassword = uiRegisterConfPassword.getText().toString();
        String email = uiRegisterEmail.getText().toString();

        boolean isValid = validate(firstName, lastName, username, password, confPassword, email);

        if(!isValid) {
            Toast.makeText(RegisterActivity.this, "Invalid input parameters!", Toast.LENGTH_SHORT).show();
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

        RegisterRequest request = new RegisterRequest(username, password, email, firstName, lastName);
        String json = new Gson().toJson(request);
        RequestBody requestBody = RequestBody.create(MediaType.parse("application/json"), json);

        Call<ResponseBody> call = apiRetrofit.Register(requestBody);

        call.enqueue(new Callback<ResponseBody>() {
            @Override
            public void onResponse(Call<ResponseBody> call, Response<ResponseBody> response) {
                if (response.isSuccessful()) {
                    try {
                        JsonParser parser = new JsonParser();
                        JsonElement mJson =  parser.parse(response.body().string());
                        Gson gson = new Gson();
                        RegisterResponse object = gson.fromJson(mJson, RegisterResponse.class);

                        Toast.makeText(RegisterActivity.this, object.getMessage(), Toast.LENGTH_LONG).show();
                        String responseString = response.body().string();
                        // do something with the response string
                        Log.d("Response",responseString);
                        Intent intent = new Intent(RegisterActivity.this, MainActivity.class);
                        startActivity(intent);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                } else {
                    int statusCode = response.code();
                    if (response.code() == 400 || response.code() == 409) {
                        try {
                            Toast.makeText(RegisterActivity.this, response.errorBody().string(), Toast.LENGTH_LONG).show();
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
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
    }

    private boolean validate(String firstName, String lastName, String username, String password, String confPassword, String email) {
        if (firstName.isEmpty() || lastName.isEmpty() || firstName.length() > 40
                || lastName.length() > 40 || username.isEmpty() || password.length() < 6
                || email.isEmpty() || !password.equals(confPassword)) {
            return false;
        }

        return true;
    }
}



//retrofit = new Retrofit.Builder().baseUrl(baseUrl).build();
//.addConverterFactory(GsonConverterFactory.create())
//apiRetrofit = retrofit.create(LandmarksApiRetrofit.class);
//JSONObject jsonBody = new JSONObject();
//try {
//    jsonBody.put("userName", username);
//    jsonBody.put("password", password);
//    jsonBody.put("email", email);
//    jsonBody.put("firstName", firstName);
//   jsonBody.put("lastName", lastName);
//} catch (JSONException e) {
//    e.printStackTrace();
//}
//RequestBody request = RequestBody.create(MediaType.parse("application/json"), jsonBody.toString());

//Call<RegisterResponse> call = apiRetrofit.Register(request);
//call.enqueue(new Callback<RegisterResponse>() {
//    @Override
//    public void onResponse(Call<RegisterResponse> call, retrofit2.Response<RegisterResponse> response) {
//        Toast.makeText(RegisterActivity.this, response.body().message, Toast.LENGTH_LONG).show();
//    }
//    @Override
//    public void onFailure(Call<RegisterResponse> call, Throwable t) {
//        uiErrorRegister.setText("Error occured!");
//    }
//});



//JSONObject newUser = new JSONObject();
//try {
//    newUser.put("userName", username);
//    newUser.put("password", password);
//    newUser.put("email", email);
//    newUser.put("firstName", firstName);
//    newUser.put("lastName", lastName);
//
//    JsonObjectRequest jsonObjectRequest = new JsonObjectRequest(Request.Method.POST, registerUrl,
//            newUser, new Response.Listener<JSONObject>() {
//        @Override
//        public void onResponse(JSONObject response) {
//            Toast.makeText(RegisterActivity.this, response.toString(), Toast.LENGTH_LONG).show();
//        }
//    }, new Response.ErrorListener() {
//        @Override
//        public void onErrorResponse(VolleyError error) {
//            uiErrorRegister.setText(error.getMessage().toString());
//        }
//    });
//
//    RequestQueue requestQueue = Volley.newRequestQueue(RegisterActivity.this);
//    requestQueue.add(jsonObjectRequest);
//
//    //Toast.makeText(RegisterActivity.this, "Successful register! Check your e-mail for further details and profile verification!", Toast.LENGTH_LONG).show();
//    //Intent intent = new Intent(RegisterActivity.this, MainActivity.class);
//    //startActivity(intent);
//} catch (JSONException e) {
//    e.printStackTrace();
//}
