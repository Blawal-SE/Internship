package com.example.helloworld.RetrofitApi;

import com.example.helloworld.jsonPlaceHolderApi;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class GetRetroFit {
    OkHttpClient okHttpClient =  UnsafeOkHttpClient.getUnsafeOkHttpClient();
    Retrofit retrofit = new Retrofit.Builder()
            .baseUrl("https://192.168.1.11:45459/")
            .client(okHttpClient)
            .addConverterFactory( GsonConverterFactory.create())
            .build();

    public jsonPlaceHolderApi GetJsonPlaceHolder()
    {
        return  retrofit.create(com.example.helloworld.jsonPlaceHolderApi.class);
    }

}
