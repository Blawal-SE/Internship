package com.example.helloworld;
import com.example.helloworld.DTO.CourseDto;
import com.example.helloworld.DTO.ImageResponseDto;
import com.example.helloworld.DTO.StudentDto;
import com.example.helloworld.DTO.UserResponse;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;
import retrofit2.http.Query;
import retrofit2.http.QueryMap;

public interface jsonPlaceHolderApi {
    @POST("Login")
    @FormUrlEncoded
    Call<UserResponse> login(@Field("username") String username,
                             @Field("password") String password,
                             @Field("grant_type") String granttype);

    @GET("api/Student")
    Call<List<StudentDto>> getStudents(@Header("Authorization") String token);

    @GET("api/Course")
    Call<List<CourseDto>> getCourses(@Header("Authorization") String token);
    @POST("api/Student")
    Call<StudentDto> AddStudent(@Header("Authorization") String token,@Body StudentDto student);
    @GET("api/Student/{id}")
    Call<StudentDto> GetStudent(@Path("id") int id, @Header("Authorization") String token);
    @DELETE("api/Student/{id}")
    Call<Boolean> DeleteStudent(@Path("id") int id, @Header("Authorization") String token);
    @PUT("api/Student")
    Call<Boolean> EditStudent(@Header("Authorization") String token ,@Body StudentDto student);
    @POST("api/Upload/imageandroid")
    Call<ImageResponseDto> ImageUpload(@Header("Authorization") String token ,@Body ImageResponseDto obj);
}

