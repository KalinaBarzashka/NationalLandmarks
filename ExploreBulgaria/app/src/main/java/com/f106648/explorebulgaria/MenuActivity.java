package com.f106648.explorebulgaria;

import androidx.annotation.CallSuper;
import androidx.appcompat.app.AppCompatActivity;

import android.app.ActivityManager;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.Toast;

public abstract class MenuActivity extends AppCompatActivity {

    private SharedPreferences.Editor sharedPreferencesEditor;

    private Menu menu;

    @CallSuper
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu, menu);

        this.menu = menu;

        SharedPreferences sharedPreferences = getApplicationContext().getSharedPreferences("TokenPref", MODE_PRIVATE);
        sharedPreferencesEditor = sharedPreferences.edit();

        return true;
    }

    @CallSuper
    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {

        switch (item.getItemId())
        {
            case (R.id.menu_action_music):
                MenuItem musicItem = menu.findItem(R.id.menu_action_music);
                if(isMyServiceRunning()) {
                    musicItem.setIcon(R.drawable.ic_action_play);
                    stopService(new Intent(this, MusicService.class));
                } else {
                    musicItem.setIcon(R.drawable.ic_action_stop);
                    startService(new Intent(this, MusicService.class));
                }
                return true;
            case (R.id.menu_landmarks):
                Intent homeIntent = new Intent(MenuActivity.this, HomePageActivity.class);
                startActivity(homeIntent);
                return true;
            case (R.id.menu_visited_landmarks):
                Toast.makeText(MenuActivity.this, "To be implemented!", Toast.LENGTH_SHORT).show();
                return true;
            case (R.id.menu_profile):
                Intent profileIntent = new Intent(MenuActivity.this, ProfileActivity.class);
                startActivity(profileIntent);
                return true;
            case (R.id.menu_logout):
                /* Clear token */
                sharedPreferencesEditor.clear();
                /* Commit the changes (add token to the file) */
                sharedPreferencesEditor.commit();
                stopService(new Intent(this, MusicService.class));
                Intent logoutIntent = new Intent(MenuActivity.this, MainActivity.class);
                startActivity(logoutIntent);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    private boolean isMyServiceRunning() {
        ActivityManager manager = (ActivityManager) getSystemService(Context.ACTIVITY_SERVICE);
        for (ActivityManager.RunningServiceInfo service : manager.getRunningServices(Integer.MAX_VALUE)) {
            if (MusicService.class.getName().equals(service.service.getClassName())) {
                return true;
            }
        }
        return false;
    }
}