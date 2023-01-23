package com.infm308.explorebulgaria;

import okhttp3.RequestBody;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Headers;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface LandmarksApiRetrofit {
    @POST("/Identity/Register")
    Call<ResponseBody> Register(@Body RequestBody request);

    @POST("/Identity/Login")
    Call<ResponseBody> Login(@Body RequestBody request);

    @Headers({"Accept: application/json"})
    @GET("/Landmark/1")
    Call<ResponseBody> GetLandmarks();

    @Headers({"Accept: application/json"})
    @GET("/Landmark/details/{id}")
    Call<ResponseBody> GetLandmarkDetails(@Path("id") String id);

    @Headers({"Accept: application/json"})
    @GET("/Identity/Profile")
    Call<ResponseBody> GetUserProfile();
}
