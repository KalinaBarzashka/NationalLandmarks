package com.f106648.explorebulgaria;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.widget.TextView;

import com.f106648.explorebulgaria.models.User;

public class ProfileActivity extends MenuActivity {

    private DatabaseHelper dbHelper;

    private long userid;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);

        TextView firstName = findViewById(R.id.firstname);
        TextView lastName = findViewById(R.id.lastname);
        TextView username = findViewById(R.id.username);
        TextView email = findViewById(R.id.email);

        SharedPreferences sharedPreferences = getApplicationContext().getSharedPreferences("TokenPref", MODE_PRIVATE);

        if(sharedPreferences != null) {
            userid = sharedPreferences.getLong("userid", 0);
        }

        dbHelper = new DatabaseHelper(this);
        User user = dbHelper.getUser(userid);

        firstName.setText("First name: " + user.getFitstName());
        lastName.setText("Last name: " + user.getLastName());
        username.setText("Username: " + user.getUsername());
        email.setText("E-mail: " + user.getEmail());
    }

    @Override
    protected void onDestroy() {
        // затваряме връзката с БД
        dbHelper.close();
        super.onDestroy();
    }

}