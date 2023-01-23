package com.infm308.explorebulgaria;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import androidx.annotation.Nullable;

import com.infm308.explorebulgaria.models.User;

public class DatabaseHelper extends SQLiteOpenHelper {
    private static final String DATABASE_NAME="explore_buldaria.db";
    private static final int DATABASE_VERSION = 1;
    public static final String TABLE_NAME = "users";
    public final static String UID = "_ID";
    public static final String COLUMN_USERNAME = "username";
    public static final String COLUMN_FIRST_NAME = "firstname";
    public static final String COLUMN_LAST_NAME = "lastname";
    public static final String COLUMN_EMAIL = "email";

    public DatabaseHelper(@Nullable Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        String SQL_CREATE_USER_TABLE = "CREATE TABLE " + TABLE_NAME +
                " (" + UID + " INTEGER PRIMARY KEY AUTOINCREMENT, "
                + COLUMN_USERNAME + " TEXT NOT NULL, " +
                COLUMN_FIRST_NAME + " TEXT NOT NULL, " +
                COLUMN_LAST_NAME + " TEXT NOT NULL, " +
                COLUMN_EMAIL + " TEXT NOT NULL " +
                ");";

        db.execSQL(SQL_CREATE_USER_TABLE);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_NAME);
        onCreate(db);
    }

    public long addUser(String username, String firstName, String lastName, String Email) {
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put(COLUMN_USERNAME, username);
        contentValues.put(COLUMN_FIRST_NAME, firstName);
        contentValues.put(COLUMN_LAST_NAME, lastName);
        contentValues.put(COLUMN_EMAIL, Email);
        long result = db.insert(TABLE_NAME, null, contentValues);
        db.close();

        return result;
    }

    public User getUser(long id) {
        SQLiteDatabase db = this.getReadableDatabase();
        String[] columns = new String[]{ COLUMN_USERNAME, COLUMN_FIRST_NAME, COLUMN_LAST_NAME, COLUMN_EMAIL };
        String selection = UID + "=?";
        String[] selectionArgs = new String[]{ String.valueOf(id) };
        Cursor cursor = db.query(TABLE_NAME,
                columns,
                selection,
                selectionArgs,
                null,
                null,
                null);

        String fname = "";
        String lname = "";
        String username = "";
        String email = "";

        if(cursor != null)
        {
            if (cursor.moveToFirst()) {
                username = cursor.getString(0);
                fname = cursor.getString(1); // DeviceID
                lname = cursor.getString(2); // EmailID
                email = cursor.getString(3); // Operator
            }
            cursor.close();
        }

        User user = new User(fname, lname, username, email);

        if (cursor != null) {
            cursor.close();
        }
        db.close();

        return user;
    }
}
