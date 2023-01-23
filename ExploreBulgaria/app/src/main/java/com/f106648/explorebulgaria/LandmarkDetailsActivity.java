package com.f106648.explorebulgaria;

import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;

import com.google.gson.Gson;
import com.google.gson.JsonElement;
import com.google.gson.JsonParser;
import com.f106648.explorebulgaria.async.GetLandmarkImageAsync;
import com.f106648.explorebulgaria.servicemodels.LandmarkDetailsResponse;

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

public class LandmarkDetailsActivity extends MenuActivity {

    private String landmarkId;
    private String token;
    private double latitude;
    private double longitude;

    private final String baseUrl = "http://10.0.2.2:8080";

    ImageView image;
    TextView name;
    TextView description;
    TextView placeName;
    TextView address;
    TextView website;
    TextView email;
    TextView phone;
    Button maps;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_landmark_details);

        landmarkId = getIntent().getStringExtra("landmarkId");
        image = findViewById(R.id.landmark_image);
        name = findViewById(R.id.name);
        description = findViewById(R.id.multiline_description);
        placeName = findViewById(R.id.place_name);
        address = findViewById((R.id.address));
        website = findViewById((R.id.website));
        email = findViewById((R.id.email));
        phone = findViewById((R.id.phone));
        maps = findViewById(R.id.btnViewOnMaps);
        Initialize();
    }

    private void Initialize() {
        SharedPreferences sharedPreferences = getApplicationContext().getSharedPreferences("TokenPref", MODE_PRIVATE);

        if(sharedPreferences != null) {
            token = sharedPreferences.getString("token", "kur");
        }

        OkHttpClient okHttpClient = new OkHttpClient.Builder().addInterceptor(new Interceptor() {
                    @NonNull
                    @Override
                    public okhttp3.Response intercept(@NonNull Chain chain) throws IOException {
                        Request newRequest  = chain.request().newBuilder()
                                .addHeader("Authorization", "Bearer " + token)
                                .build();
                        return chain.proceed(newRequest);
                    }
                })
                .readTimeout(60, TimeUnit.SECONDS)
                .connectTimeout(60, TimeUnit.SECONDS)
                .build();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(baseUrl)
                .addConverterFactory(GsonConverterFactory.create())
                .client(okHttpClient)
                .build();

        LandmarksApiRetrofit apiRetrofit = retrofit.create(LandmarksApiRetrofit.class);

        Call<ResponseBody> call = apiRetrofit.GetLandmarkDetails(landmarkId);
        call.enqueue(new Callback<ResponseBody>() {
            @Override
            public void onResponse(@NonNull Call<ResponseBody> call, @NonNull Response<ResponseBody> response) {
                if (response.isSuccessful()) {
                    try {
                        JsonParser parser = new JsonParser();
                        JsonElement mJson =  parser.parse(response.body().string());
                        Gson gson = new Gson();
                        LandmarkDetailsResponse object = gson.fromJson(mJson, LandmarkDetailsResponse.class);

                        name.setText(object.getName());
                        description.setText(object.getDescription());
                        placeName.setText(object.getPlaceName());
                        address.setText(object.getAddress() != null ? "Address: " + object.getAddress() : "Address: No info provided!");
                        website.setText(object.getWebsite() != null ? "Website: " + object.getWebsite() : "Website: No info provided!");
                        email.setText(object.getEmail() != null ? "E-mail: " + object.getEmail() : "E-mail: No info provided!");
                        phone.setText(object.getPhone() != null ? "Phone: " + object.getPhone() : "Phone: No info provided!");
                        new GetLandmarkImageAsync(baseUrl + "/" + object.getImagePath(), image).execute();
                        latitude = object.getLatitude();
                        longitude = object.getLongitude();

                        String responseString = response.body().string();
                        // do something with the response string
                        Log.d("Response",responseString);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                } else {
                    int statusCode = response.code();
                    if (response.code() == 404) {
                        try {
                            Toast.makeText(LandmarkDetailsActivity.this, response.errorBody().string(), Toast.LENGTH_LONG).show(); //fix
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                    // handle the error here
                    Log.e("Error","Error with status code:"+statusCode);
                }
            }
            @Override
            public void onFailure(@NonNull Call<ResponseBody> call, Throwable t) {
                // handle the error here
                Log.e("Error",t.getMessage());
            }
        });
    }

    public void openGoogleMaps(View view){
        Uri gmmIntentUri = Uri.parse(" geo: " + latitude + ", " + longitude);
        Intent mapIntent = new Intent(Intent.ACTION_VIEW, gmmIntentUri);
        mapIntent.setPackage("com.google.android.apps.maps");
        if (mapIntent.resolveActivity(getPackageManager()) != null) {
            startActivity(mapIntent);
        } else {
            Toast.makeText(LandmarkDetailsActivity.this, "No maps found!", Toast.LENGTH_SHORT).show();
        }
    }
}