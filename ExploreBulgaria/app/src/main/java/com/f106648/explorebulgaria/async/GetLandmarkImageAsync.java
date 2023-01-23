package com.f106648.explorebulgaria.async;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.util.Log;
import android.widget.ImageView;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;

public class GetLandmarkImageAsync extends AsyncTask<Void, Void, Bitmap>  {

    private String src;
    private Bitmap bitmap;
    private ImageView image;

    public GetLandmarkImageAsync(String src, ImageView image) {
        super();
        this.src = src;
        this.image = image;
    }

    @Override
    protected Bitmap doInBackground(Void... voids) {
        try {
            Log.e("src",src);
            URL url = new URL(src);
            HttpURLConnection connection = (HttpURLConnection) url.openConnection();
            connection.setDoInput(true);
            connection.connect();
            InputStream input = connection.getInputStream();
            bitmap = BitmapFactory.decodeStream(input);
            Log.e("Bitmap","returned");
            return bitmap;
        } catch (IOException e) {
            e.printStackTrace();
            Log.e("Exception",e.getMessage());
            return null;
        }
    }

    @Override
    protected void onPostExecute(Bitmap result) {
        super.onPostExecute(result);
        image.setImageBitmap(result);
    }
}
