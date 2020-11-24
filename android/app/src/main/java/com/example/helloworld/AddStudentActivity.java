package com.example.helloworld;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.media.Image;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.util.Base64;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.helloworld.DTO.CourseDto;
import com.example.helloworld.DTO.ImageResponseDto;
import com.example.helloworld.DTO.StudentDto;
import com.example.helloworld.RetrofitApi.GetRetroFit;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AddStudentActivity extends AppCompatActivity {
    private Button coursespiner, addStudent, uploadimage;
    private ImageView imageView;
    private Bitmap bitmap;
    EditText name, fname, email, phone, password, confirmpassword;
    DatePicker dob;
    int id;
    String ImagePath;
    ArrayList<Integer> userItems = new ArrayList<>();
    public String[] Courses;
    public ArrayList<Integer> CoursesWithIds = new ArrayList<>();
    private List<CourseDto> AllCourseDto;
    private boolean[] checkedItems;

    @Override
    public void onBackPressed() {
        Intent a = new Intent(this, HomeActivity.class);
        a.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(a);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_student);
        coursespiner = findViewById(R.id.btncourseselect);
        addStudent = findViewById(R.id.btnadd);
        imageView = findViewById(R.id.imageView);
        uploadimage = findViewById(R.id.btnimage);
        id = getIntent().getIntExtra("id", 0);
        if (id != 0) {
            EditStudentSetValuesInFields(id);
        }
        Call<List<CourseDto>> call = new GetRetroFit().GetJsonPlaceHolder().getCourses(GetToken());
        call.enqueue(new Callback<List<CourseDto>>() {
            @Override
            public void onResponse(Call<List<CourseDto>> call, Response<List<CourseDto>> response) {
                AllCourseDto = response.body();
                int i = 0;
                String[] newarr = new String[AllCourseDto.size()];
                for (CourseDto course : AllCourseDto) {
                    newarr[i] = course.Name.toString();
                    CoursesWithIds.add(course.CourseId);
                    i++;
                }
                Courses = newarr;
            }

            @Override
            public void onFailure(Call<List<CourseDto>> call, Throwable t) {
                String Error = t.getMessage();
            }
        });
        coursespiner.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {

                checkedItems = new boolean[Courses.length];
                AlertDialog.Builder builder = new AlertDialog.Builder(AddStudentActivity.this);
                builder.setTitle("Select Course from List");
                builder.setMultiChoiceItems(Courses, checkedItems, new DialogInterface.OnMultiChoiceClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int position, boolean isChecked) {
                        if (isChecked) {
                            if (!userItems.contains(CoursesWithIds.get(position).intValue())) {
                                userItems.add(CoursesWithIds.get(position).intValue());
                            } else {
                                userItems.remove(CoursesWithIds.get(position).intValue());
                            }
                        } else {
                            userItems.remove(position);
                        }
                    }
                });
                builder.setCancelable(false);
                builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {

                    }
                });
                builder.setNegativeButton("cancel", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.dismiss();
                    }
                });
                builder.setNeutralButton("clear All", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        // for(int i=0;i<checkedItems.length;i++)
                        // {
                        //      checkedItems[i]=false;
                        //  }
                    }
                });
                AlertDialog dialog = builder.create();
                dialog.show();
            }
        });
        uploadimage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent();
                intent.setType("image/*");
                intent.setAction(Intent.ACTION_GET_CONTENT);
                startActivityForResult(intent, 21);
            }

        });

        addStudent.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                AddStudent();
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == 21 && resultCode == RESULT_OK && data != null) {
            Uri path = data.getData();
            try {
                bitmap = MediaStore.Images.Media.getBitmap(getContentResolver(), path);
                imageView.setImageBitmap(bitmap);
            } catch (IOException e) {
                e.printStackTrace();
            }

        }
    }

    public boolean Validation(EditText name, EditText fname, EditText password, EditText confirmpassword, EditText email) {
       boolean result=true;
        if (name.getText().toString().length() == 0) {
            name.setError("Name is Required");
            result= false;
        }  if (fname.getText().toString().length() == 0) {
            fname.setError("Father Name is Required");
            result= false;
        }  if (email.getText().toString().length() == 0) {
            fname.setError("Email is Required");
            result= false;
        }  if (password.getText().toString() != confirmpassword.getText().toString()) {
            confirmpassword.setError("password does not match");
            result= false;
        }
            return result;

    }

    ;

    public void addStudentServerCall() {
        name = findViewById(R.id.etxtName);
        fname = findViewById(R.id.etxtfname);
        phone = findViewById(R.id.etxtphone);
        dob = (DatePicker) findViewById(R.id.pickerdob);
        email = findViewById(R.id.etxtemail);
        password = findViewById(R.id.etxtpassword);
        confirmpassword = findViewById(R.id.etxtcpassword);
        if (Validation(name, fname, password, confirmpassword, email)) {
            StudentDto student = new StudentDto();
            student.setName(name.getText().toString());
            student.setPhone(phone.getText().toString());
            student.setFName(fname.getText().toString());
            student.setDob(dob.getDayOfMonth() + "/" + dob.getMonth() + "/" + dob.getYear());
            student.setEmail(email.getText().toString());
            student.setCourses(userItems);
            student.setEmail(email.getText().toString());
            student.setPassword(password.getText().toString());
            student.setImageUrl(ImagePath != null && ImagePath != "" ? ImagePath : "");
            if (id != 0) {
                Call<Boolean> call = new GetRetroFit().GetJsonPlaceHolder().EditStudent(GetToken(), student);
                call.enqueue(new Callback<Boolean>() {
                    @Override
                    public void onResponse(Call<Boolean> call, Response<Boolean> response) {

                    }

                    @Override
                    public void onFailure(Call<Boolean> call, Throwable t) {

                    }
                });
            }
            Call<StudentDto> call = new GetRetroFit().GetJsonPlaceHolder().AddStudent(GetToken(), student);
            call.enqueue(new Callback<StudentDto>() {
                @Override
                public void onResponse(Call<StudentDto> call, Response<StudentDto> response) {
                    StudentDto student = response.body();
                }

                @Override
                public void onFailure(Call<StudentDto> call, Throwable t) {

                }
            });
        }
    }

    public void AddStudent() {
        if (bitmap == null ) {
            addStudentServerCall();
        }else{
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.JPEG, 75, byteArrayOutputStream);
        byte[] imageinbyte = byteArrayOutputStream.toByteArray();
        String base64encoded = Base64.encodeToString(imageinbyte, Base64.DEFAULT);
        ImageResponseDto dto = new ImageResponseDto();
        dto.setImageBase64(base64encoded);

            Call<ImageResponseDto> call = new GetRetroFit().GetJsonPlaceHolder().ImageUpload(GetToken(), dto);
            call.enqueue(new Callback<ImageResponseDto>() {
                @Override
                public void onResponse(Call<ImageResponseDto> call, Response<ImageResponseDto> response) {
                    ImageResponseDto imagepaths = response.body();
                    ImagePath = imagepaths.getRealPath().toString();
                    addStudentServerCall();
                }

                @Override
                public void onFailure(Call<ImageResponseDto> call, Throwable t) {

                }
            });
        }
    }

    public void EditStudentSetValuesInFields(int StudentId) {
        Call<StudentDto> call = new GetRetroFit().GetJsonPlaceHolder().GetStudent(StudentId, GetToken());
        call.enqueue(new Callback<StudentDto>() {
            @Override
            public void onResponse(Call<StudentDto> call, Response<StudentDto> response) {
                StudentDto student = response.body();
                name = findViewById(R.id.etxtName);
                fname = findViewById(R.id.etxtfname);
                phone = findViewById(R.id.etxtphone);
                dob = (DatePicker) findViewById(R.id.pickerdob);
                email = findViewById(R.id.etxtemail);
                password = findViewById(R.id.etxtpassword);
                name.setText(student.getName().toString(), TextView.BufferType.EDITABLE);
                fname.setText(student.getFName().toString(), TextView.BufferType.EDITABLE);
                email.setText(student.getEmail().toString(), TextView.BufferType.EDITABLE);
                phone.setText(student.getPhone().toString(), TextView.BufferType.EDITABLE);
                password.setText(student.getPassword().toString(), TextView.BufferType.EDITABLE);
                ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
                byte[] imageBytes = byteArrayOutputStream.toByteArray();
                imageBytes = Base64.decode(student.getImageBase64(), Base64.DEFAULT);
                bitmap = BitmapFactory.decodeByteArray(imageBytes, 0, imageBytes.length);

                imageView.setImageBitmap(bitmap);

            }

            @Override
            public void onFailure(Call<StudentDto> call, Throwable t) {

            }
        });

    }

    public String GetToken() {
        SharedPreferences sharedpreferences = getSharedPreferences("userpreference", Context.MODE_PRIVATE);
        String str = sharedpreferences.getString("token", "");
        return str;
    }
}