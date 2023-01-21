package com.infm308.explorebulgaria;

import androidx.annotation.CallSuper;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.Toast;

public abstract class MenuActivity extends AppCompatActivity {

    @CallSuper
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu, menu);
        return true;
    }

    @CallSuper
    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {

        switch (item.getItemId())
        {
            case R.id.menu_landmarks:
                Toast.makeText(MenuActivity.this, "You Clicked Landmarks", Toast.LENGTH_SHORT).show();
                return true;
            case R.id.menu_visited_landmarks:
                Toast.makeText(MenuActivity.this, "You Clicked Visited landmarks", Toast.LENGTH_SHORT).show();
                return true;
            case R.id.menu_profile:
                Toast.makeText(MenuActivity.this, "You Clicked Profile", Toast.LENGTH_SHORT).show();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }
}