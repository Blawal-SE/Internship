package com.example.helloworld;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.example.helloworld.DTO.UserResponse;
import com.example.helloworld.RetrofitApi.GetRetroFit;
import com.example.helloworld.RetrofitApi.UnsafeOkHttpClient;

import okhttp3.OkHttpClient;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class Login extends AppCompatActivity {
           private EditText username;
           private  EditText password;

           OkHttpClient okHttpClient =  UnsafeOkHttpClient.getUnsafeOkHttpClient();
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        //get every field from layout
        Button register=(Button) findViewById(R.id.btnRegister);
        Button login=findViewById(R.id.btnLogin);
        username=findViewById(R.id.etxtUserName);
        password=findViewById(R.id.etxtPassword);


        Intent RegisterIntent=new Intent(this,RegisterActivity.class);
        Intent studentlistintent=new Intent(this,StudentListActivity.class);
        Intent HomeIntent=new Intent(this,HomeActivity.class);

        login.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Call<UserResponse> call= new GetRetroFit().GetJsonPlaceHolder().login(username.getText().toString(),password.getText().toString(),"password");
               // Retrofit retrofit = new Retrofit.Builder()
                        //.baseUrl("https://192.168.1.5:45455/")
                       // .client(okHttpClient)
                      //  .addConverterFactory( GsonConverterFactory.create())
                       // .build();
               // jsonPlaceHolderApi jsonPlaceHolderapi=retrofit.create(com.example.helloworld.jsonPlaceHolderApi.class);
               // Call<UserResponse> call= jsonPlaceHolderapi.login(username.getText().toString(),password.getText().toString(),"password");
                call.enqueue(new Callback<UserResponse>() {
                    @Override
                    public void onResponse(Call<UserResponse> call, Response<UserResponse> response) {
                        UserResponse user=response.body();
                        SharedPreferences sharedpreferences = getSharedPreferences("userpreference", Context.MODE_PRIVATE);
                        SharedPreferences.Editor editor = sharedpreferences.edit();;
                        editor.putString("token","bearer "+user.getAccess_token());
                        editor.commit();
                        startActivity(HomeIntent);
                        finish();
                    }

                    @Override
                    public void onFailure(Call<UserResponse> call, Throwable t) {
                          String error=t.getMessage();
                    }

                });

            }
        });

        register.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                startActivity(RegisterIntent);
                finish();
            }
        });

    }

}