package com.example.helloworld;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.graphics.Color;
import com.example.helloworld.DTO.StudentDto;
import com.example.helloworld.RetrofitApi.GetRetroFit;
import com.example.helloworld.RetrofitApi.UnsafeOkHttpClient;

import java.util.List;

import okhttp3.OkHttpClient;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class StudentListActivity extends AppCompatActivity {
    @Override
    public void onBackPressed(){
        Intent a = new Intent(this,HomeActivity.class);
        a.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(a);
    }
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_student_list);
        Call<List<StudentDto>> call= new GetRetroFit().GetJsonPlaceHolder().getStudents(GetToken());
        call.enqueue(new Callback<List<StudentDto>>() {
            @Override
            public void onResponse(Call<List<StudentDto>> call, Response<List<StudentDto>> response) {
               LinearLayout layout=findViewById(R.id.linearlayout);
              if(!response.isSuccessful()){
                  Log.e("Response",response.message());
                   return;
              }
              List<StudentDto> students=response.body();
              for(StudentDto student:students)
              {

                  String content = "";
                  content += "Student Name: \t" + student.getName()+"\n";
                  content += "Student FName: \t" + student.getFName()+ "\n";
                  content += "Student DOB: \t" + student.getDob()+ "\n";
                  content += "Student Phone: \t" + student.getPhone()+ "\n";
                  content += "Student Password: \t" + student.getPassword()+ "\n";
                  content += "Student Courses: \t" + student.getCoursesCount()+ "\n";
                  TextView studentTextView = new TextView(StudentListActivity.this);
                  LinearLayout.LayoutParams lparams = new LinearLayout.LayoutParams(
                          LinearLayout.LayoutParams.WRAP_CONTENT, LinearLayout.LayoutParams.WRAP_CONTENT);

                  studentTextView.setLayoutParams(lparams);
                  studentTextView.setText(content);
                  layout.addView(studentTextView);
                  addButton("Edit",layout,lparams, student.getId());
                  addButton("Delete",layout,lparams,student.getId());
              }
            }

            @Override
            public void onFailure(Call<List<StudentDto>> call, Throwable t) {
                Log.e("Error",t.getMessage());
            }
        });

    }
    public void edit(int id){
        Intent intent = new Intent(StudentListActivity.this,AddStudentActivity.class);
        intent.putExtra("id",id);
        startActivity(intent);
        finish();
    }
    public void delete(int id){
        //Call<String> call = studentApi.deleteStudent(id,"bearer " + token);
        Call<Boolean> call=new GetRetroFit().GetJsonPlaceHolder().DeleteStudent(id,GetToken());
        call.enqueue(new Callback<Boolean>() {
            @Override
            public void onResponse(Call<Boolean> call, Response<Boolean> response) {
                Intent intent = new Intent(StudentListActivity.this,StudentListActivity.class);
                startActivity(intent);
                finish();
            }

            @Override
            public void onFailure(Call<Boolean> call, Throwable t) {
                    Log.e("Error", t.getMessage());
            }
        });
    }
    public void addButton(String dataToDisplay, LinearLayout linearLayout, LinearLayout.LayoutParams lparams, int studentId){
        Button button = new Button(StudentListActivity.this);
        button.setText(dataToDisplay);
      //  button.setTextColor(getResources().getColor().);

        button.setTextColor(Color.BLACK);

        if(dataToDisplay == "Delete"){
            button.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    delete(studentId);
                }
            });
        }else{
            button.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    edit(studentId);
                }
            });
        }

        linearLayout.addView(button);
    }

    public String GetToken(){

        SharedPreferences sharedpreferences = getSharedPreferences("userpreference", Context.MODE_PRIVATE);
        String str = sharedpreferences.getString("token", "");
       return str;
    }
}