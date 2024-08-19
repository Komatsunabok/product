package com.example.trainingcounter;

import android.content.Intent;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.media.AudioAttributes;
import android.media.SoundPool;
import android.os.Bundle;

import com.google.android.material.snackbar.Snackbar;

import androidx.appcompat.app.AppCompatActivity;

import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.TextView;

import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import com.example.trainingcounter.databinding.ActivityCountBinding;

import java.util.Objects;

public class CountActivity extends AppCompatActivity implements SensorEventListener {

    private AppBarConfiguration appBarConfiguration;
    private ActivityCountBinding binding;

    private TextView mRollText;
    private int count = 0;
    private int countFlag = 0;
    private double roll = 0;
    private String position;
    private int fromDegree;
    private int toDegree;
    private float[] mAccelerationValue = new float[3];
    private float[] mGeoMagneticValue = new float[3];
    private float[] mOrientationValue = new float[3];
    private float[] mInRotationMatrix = new float[9];
    private float[] mOutRotationMatrix = new float[9];
    private float[] mInclinationMatrix = new float[9];
    private SoundPool soundPool;
    private int soundId;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
//        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_count);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main_count), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        Intent intent = getIntent();
        position = intent.getStringExtra("POSITION");
        fromDegree = intent.getIntExtra("FROM_DEGREE", 0);
        toDegree = intent.getIntExtra("TO_DEGREE", 60);

        SensorManager sensorManager = (SensorManager) getSystemService(SENSOR_SERVICE);
        Sensor accelerationSensor = sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        Sensor magneticSensor = sensorManager.getDefaultSensor(Sensor.TYPE_MAGNETIC_FIELD);

        sensorManager.registerListener(this, accelerationSensor, SensorManager.SENSOR_DELAY_UI);
        sensorManager.registerListener(this, magneticSensor, SensorManager.SENSOR_DELAY_UI);
        mRollText = (TextView) findViewById(R.id.text_count);

        Button buttonReset = (Button) findViewById(R.id.button_reset);
        buttonReset.setOnClickListener(new View.OnClickListener() {
            public void onClick(View arg0) {
                count = 0;
            }
        });

        Button buttonFinish = (Button) findViewById(R.id.button_finish);
        buttonFinish.setOnClickListener(new View.OnClickListener() {
            public void onClick(View arg0) {
                Intent intent = new Intent(getApplicationContext(), MainActivity.class);
                startActivity(intent);
            }
        });

        AudioAttributes audioAttributes = new AudioAttributes.Builder()
                .setUsage(AudioAttributes.USAGE_ASSISTANCE_SONIFICATION)
                .setContentType(AudioAttributes.CONTENT_TYPE_SONIFICATION)
                .build();
        soundPool = new SoundPool.Builder()
                .setMaxStreams(1)
                .setAudioAttributes(audioAttributes)
                .build();
        soundId = soundPool.load(this, R.raw.maou_se_system38, 1);

        Log.d("SOUND", "soundId: "+soundId);
    }

    @Override
    public boolean onSupportNavigateUp() {
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment_content_count);
        return NavigationUI.navigateUp(navController, appBarConfiguration)
                || super.onSupportNavigateUp();
    }

    @Override
    public void onSensorChanged(SensorEvent sensorEvent){
        switch(sensorEvent.sensor.getType()){
            case Sensor.TYPE_MAGNETIC_FIELD:mGeoMagneticValue = sensorEvent.values.clone();break;
            case Sensor.TYPE_ACCELEROMETER:mAccelerationValue = sensorEvent.values.clone();break;
        }
        SensorManager.getRotationMatrix(mInRotationMatrix, mInclinationMatrix, mAccelerationValue, mGeoMagneticValue);
        SensorManager.remapCoordinateSystem(mInRotationMatrix, SensorManager.AXIS_X, SensorManager.AXIS_Z, mOutRotationMatrix);
        SensorManager.getOrientation(mOutRotationMatrix, mOrientationValue);

        roll = Math.toDegrees((double)mOrientationValue[2]);
        if (Objects.equals(position, "Right")){
            if (countFlag==0 && roll>=toDegree){
                count = count + 1;
                countFlag = 1;
                if (soundId != 0) {
                    int streamId = soundPool.play(soundId, 1, 1, 0, 0, 1);
                    Log.d("SOUND", "play result: " + streamId);
                } else {
                    Log.d("SOUND", "soundId is 0, cannot play sound");
                }
            } else if (countFlag==1 && roll<=fromDegree) {
                countFlag = 0;
            }
        }else {
            if (countFlag==0 && roll<=-toDegree){
                count = count + 1;
                countFlag = 1;
                if (soundId != 0) {
                    int streamId = soundPool.play(soundId, 1, 1, 0, 0, 1);
                    Log.d("SOUND", "play result: " + streamId);
                } else {
                    Log.d("SOUND", "soundId is 0, cannot play sound");
                }
            } else if (countFlag==1 && roll>=-fromDegree) {
                countFlag = 0;
            }
        }
        String rollText = String.valueOf(count);
        mRollText.setText(rollText);

    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        if (soundPool != null) {
            soundPool.release();
            soundPool = null;
        }
    }


    @Override
    public void onAccuracyChanged(Sensor sensor, int i){

    }
}