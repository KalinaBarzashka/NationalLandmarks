package com.infm308.explorebulgaria;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    private EditText uiLoginUsername;
    private EditText uiLoginPassword;
    private Button uiLoginBtn;
    private TextView uiAttemptsInfo;

    private final String name = "Admin";
    private final String pass = "123456";

    private boolean isValid = false;
    private int counter = 5;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        uiLoginUsername = findViewById(R.id.loginUsername);
        uiLoginPassword = findViewById(R.id.loginPassword);
        uiLoginBtn = findViewById(R.id.loginBtn);
        uiAttemptsInfo = findViewById(R.id.attemptsInfo);

        uiLoginBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String inputUsername = uiLoginUsername.getText().toString();
                String inputPassword = uiLoginPassword.getText().toString();

                if(inputUsername.isEmpty() || inputPassword.isEmpty()) {
                    Toast.makeText(MainActivity.this, "Please enter username and password!", Toast.LENGTH_SHORT).show();
                } else {
                    isValid = validate(inputUsername, inputPassword);

                    if(!isValid) {
                        counter--;
                        Toast.makeText(MainActivity.this, "Invalid username or password!", Toast.LENGTH_SHORT).show();
                        uiAttemptsInfo.setText("Number of attempts remaining: " + counter);

                        if(counter == 0) {
                            uiLoginBtn.setEnabled(false);
                        }
                    } else {
                        Toast.makeText(MainActivity.this, "Successful login!", Toast.LENGTH_SHORT).show();
                        // Add the code to go to new activity
                        Intent intent = new Intent(MainActivity.this, HomePageActivity.class);
                        startActivity(intent);
                    }
                }
            }
        });
    }

    private boolean validate(String username, String password) {
        if(username.equals(name) && password.equals(pass)) {
            return true;
        }
        return false;
    }
}