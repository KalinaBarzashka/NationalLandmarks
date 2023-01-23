package com.infm308.explorebulgaria;

import android.content.SharedPreferences;
import android.os.Bundle;

import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.infm308.explorebulgaria.async.GetLandmarksAsync;
import com.infm308.explorebulgaria.async.GetUserProfileAsync;
import com.infm308.explorebulgaria.models.Landmark;

import java.util.ArrayList;

public class HomePageActivity extends MenuActivity {

    private String token;

    public static RecyclerView recyclerView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home_page);

        SharedPreferences sharedPreferences = getApplicationContext().getSharedPreferences("TokenPref", MODE_PRIVATE);

        if(sharedPreferences != null) {
            token = sharedPreferences.getString("token", "");
        }

        recyclerView = findViewById(R.id.recycle_view);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        ArrayList<Landmark> landmarks = new ArrayList<>();

        LandmarkAdapter landmarkAdapter = new LandmarkAdapter(this, landmarks);
        recyclerView.setAdapter(landmarkAdapter);
        recyclerView.addItemDecoration(new DividerItemDecoration(this, LinearLayoutManager.VERTICAL));

        createListData();
        saveUserProfile();
    }

    private void createListData() {
        new GetLandmarksAsync(token).execute();
    }

    private void saveUserProfile() { new GetUserProfileAsync(HomePageActivity.this, token).execute(); }
}