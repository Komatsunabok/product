package com.example.trainingcounter;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import androidx.activity.EdgeToEdge;
import androidx.activity.result.ActivityResultLauncher;
import androidx.activity.result.contract.ActivityResultContracts;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import org.w3c.dom.Text;

public class MainActivity extends AppCompatActivity {
    private String position = "Right";
    private int fromDegree = 0;
    private int toDegree = 60;
    private static final int REQUEST_CODE_SETTINGS = 1;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        Intent intent = getIntent();
        position = intent.getStringExtra("POSITION");
        if (position == null){
            position = "Right";
        }
        fromDegree = intent.getIntExtra("FROM_DEGREE", 0);
        toDegree = intent.getIntExtra("TO_DEGREE", 60);

        TextView textView_position = findViewById(R.id.textView_position);
        TextView textView_fromDegree = findViewById(R.id.textView_fromDegree);
        TextView textView_toDegree = findViewById(R.id.textView_toDegree);

        Log.d("SetVal", "Main");
        Log.d("SetVal", "position "+position);
        Log.d("SetVal", "fromDegree "+fromDegree);
        Log.d("SetVal", "toDegree "+toDegree);

        textView_position.setText(position);
        textView_fromDegree.setText(String.valueOf(fromDegree));
        textView_toDegree.setText(String.valueOf(toDegree));

        Button countActivitySwitchButton = (Button)findViewById(R.id.button_start);
        countActivitySwitchButton.setOnClickListener(new View.OnClickListener(){
            public void onClick(View arg0){
                Intent intent_count = new Intent(getApplicationContext(), CountActivity.class);
                intent_count.putExtra("POSITION", position);
                intent_count.putExtra("FROM_DEGREE", fromDegree);
                intent_count.putExtra("TO_DEGREE", toDegree);
                startActivity(intent_count);
            }
        });

        Button settingActivitySwitchButton = (Button)findViewById(R.id.button_setting);
        settingActivitySwitchButton.setOnClickListener(new View.OnClickListener(){
            public void onClick(View arg0){
                Intent intent_setting = new Intent(getApplicationContext(), SettingActivity.class);
                intent_setting.putExtra("POSITION", position);
                intent_setting.putExtra("FROM_DEGREE", fromDegree);
                intent_setting.putExtra("TO_DEGREE", toDegree);
                settingsActivityResultLauncher.launch(intent_setting);
//                startActivity(intent_setting);
            }
        });
    }

    private final ActivityResultLauncher<Intent> settingsActivityResultLauncher = registerForActivityResult(
            new ActivityResultContracts.StartActivityForResult(),
            result -> {
                if (result.getResultCode() == RESULT_OK && result.getData() != null) {
                    Intent data = result.getData();
                    position = data.getStringExtra("POSITION");
                    fromDegree = data.getIntExtra("FROM_DEGREE", 0);
                    toDegree = data.getIntExtra("TO_DEGREE", 60);
                }
            });

//    @Override
//    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
//        super.onActivityResult(requestCode, resultCode, data);
//        if (requestCode == REQUEST_CODE_SETTINGS && resultCode == RESULT_OK) {
//            position = data.getStringExtra("POSITION");
//            fromDegree = data.getIntExtra("FROM_DEGREE", 0);
//            toDegree = data.getIntExtra("TO_DEGREE", 60);
//        }
//    }

}