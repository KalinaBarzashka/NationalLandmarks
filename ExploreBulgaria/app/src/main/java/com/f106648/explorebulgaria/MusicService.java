package com.f106648.explorebulgaria;

import android.app.Service;
import android.content.Intent;
import android.media.MediaPlayer;
import android.os.IBinder;
import android.widget.Toast;

public class MusicService extends Service {

    MediaPlayer mediaPlayer;

    // Извиква се, когато сървисът се създава
    @Override
    public void onCreate() {
        super.onCreate();
        mediaPlayer = MediaPlayer.create(this, R.raw.music);
        mediaPlayer.setLooping(true);
        Toast.makeText(this, "Listening to music now.", Toast.LENGTH_LONG).show();
    }

    // Извиква се, когато клиентът се свързва с bindService()
    @Override
    public IBinder onBind(Intent intent) {
        throw new UnsupportedOperationException("Not yet implemented");
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        if(mediaPlayer == null)
        {
            mediaPlayer = MediaPlayer.create(this, R.raw.music);
            mediaPlayer.setLooping(true);
        }
        mediaPlayer.start();
        return START_STICKY;
    }

    // Извиква се, когато сървисът не се използва повече и се разрушава
    @Override
    public void onDestroy() {
        super.onDestroy();
        mediaPlayer.stop();
        mediaPlayer.release();
        Toast.makeText(this, "Music stopped.", Toast.LENGTH_LONG).show();
    }
}
