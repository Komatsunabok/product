package com.example.trainingcounter;

import android.content.Intent;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorManager;
import android.os.Bundle;

import com.google.android.material.snackbar.Snackbar;

import androidx.appcompat.app.AppCompatActivity;

import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import com.example.trainingcounter.databinding.ActivitySettingBinding;

public class SettingActivity extends AppCompatActivity {

    private AppBarConfiguration appBarConfiguration;
    private ActivitySettingBinding binding;
    private RadioButton position_set;
    private String position;
    private String fromDegree;
    private String toDegree;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
//        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_setting);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main_setting), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        Intent intent = getIntent();
        position = intent.getStringExtra("POSITION");
        fromDegree = intent.getStringExtra("FROM_DEGREE");
        toDegree = intent.getStringExtra("TO_DEGREE");

        RadioGroup position_radioGroup = (RadioGroup) findViewById(R.id.radioButton_position);
        EditText fromDegree_set = (EditText) findViewById(R.id.editText_fromDegree);
        EditText toDegree_set = (EditText) findViewById(R.id.editText_toDegree);

//        if (position_radioGroup != null) {
//            if (position.equals("Right")) {
//                position_radioGroup.check(R.id.radioButton_right);
//            } else if (position.equals("Left")) {
//                position_radioGroup.check(R.id.radioButton_left);
//            }
//        }
//        fromDegree_set.setText(String.valueOf(Integer.valueOf(fromDegree)));
//        toDegree_set.setText(String.valueOf(toDegree));

        Button buttonOk = (Button) findViewById(R.id.button_ok);
        buttonOk.setOnClickListener(new View.OnClickListener() {
            public void onClick(View arg0) {
                Intent intent = new Intent(getApplicationContext(), MainActivity.class);
                position_set = findViewById(position_radioGroup.getCheckedRadioButtonId());
                position = position_set.getText().toString();
                fromDegree = fromDegree_set.getText().toString();
                toDegree = toDegree_set.getText().toString();

                intent.putExtra("POSITION", position);
                intent.putExtra("FROM_DEGREE", Integer.parseInt(fromDegree));
                intent.putExtra("TO_DEGREE", Integer.parseInt(toDegree));

                Log.d("SetVal", "buttonOk");
                Log.d("SetVal", "position"+position_set.getText());
                Log.d("SetVal", "fromDegree"+fromDegree_set.getText());
                Log.d("SetVal", "toDegree"+toDegree_set.getText());

                setResult(RESULT_OK, intent);
                startActivity(intent);
            }
        });

        Button buttonCancel = (Button) findViewById(R.id.button_cancel);
        buttonCancel.setOnClickListener(new View.OnClickListener() {
            public void onClick(View arg0) {
                Intent intent = new Intent(getApplicationContext(), MainActivity.class);
                intent.putExtra("POSITION", position);
                intent.putExtra("FROM_DEGREE", fromDegree);
                intent.putExtra("TO_DEGREE", toDegree);
//
//                Log.d("SetVal", "buttonCancel");
//                Log.d("SetVal", "position"+position);
//                Log.d("SetVal", "fromDegree"+fromDegree);
//                Log.d("SetVal", "toDegree"+toDegree);
                startActivity(intent);
            }
        });

    }

    @Override
    public boolean onSupportNavigateUp() {
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment_content_count);
        return NavigationUI.navigateUp(navController, appBarConfiguration)
                || super.onSupportNavigateUp();
    }

}