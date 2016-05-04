using UnityEngine;
using System.Collections;
using Uniduino;
using System;

public class ArduinoHandler : MonoBehaviour {

    public Player p;
    Arduino arduino;

    public GameObject canvas;

    public int pinShoot = 2;
    public int buttonValue;

    public int pinBuzzer = 3;

    public int pinPotentiometer = 1;
    public float potentiometerValue;

    public int pinTouch = 5;
    public float touchValue;

    public int pinLed = 9;

    public int testLed = 13;
    
    public bool useArduino = false;

    private float timePassed;
    private float startTime;

    private bool rndIsSet;
    private bool LEDboolean;
    int rnd = 0;

    private bool canShoot;
    float shootTimer;

    private bool hasTouched;
    public Obstacle obstacle;


    // Use this for initialization
    void Start () {
        canvas.SetActive(false);
        startTime = Time.time;

        if (p == null)
        {
            Debug.Log("Please select the ArduinoHandler and dragNdrop the player into the P in the inspector.");
        }
        else
        {
            p.GetComponent<Player>();
        }
        
        if (useArduino)
        {
            arduino = Arduino.global;
            arduino.Setup(ConfigurePins);
        }

        LEDboolean = true;
        rndIsSet = false;
        hasTouched = false;
        canShoot = true;

    }
	
	
	void ConfigurePins () {
        arduino.pinMode(pinShoot, PinMode.INPUT);
        arduino.reportDigital((byte)(pinShoot / 8), 1);
        // set the pin mode for the test LED on your board, pin 13 on an Arduino Uno
        arduino.pinMode(testLed, PinMode.OUTPUT);

        arduino.pinMode(pinTouch, PinMode.INPUT);
        arduino.reportDigital((byte)(pinTouch / 8 ), 1);

        arduino.pinMode(pinPotentiometer, PinMode.INPUT);
        arduino.reportAnalog(pinPotentiometer, 1);
    }

    void Update()
    {
        if (useArduino)
        {
            // read the value from the digital input
            buttonValue = arduino.digitalRead(pinShoot);

            touchValue = arduino.digitalRead(pinTouch);

            // apply that value to the test LED
            arduino.digitalWrite(testLed, buttonValue);

            if (buttonValue == 1 && canShoot)
            {
                p.Shoot();
                canShoot = false;
                shootTimer = Time.time;
            }

            if(Time.time > shootTimer + 1)
            {
                canShoot = true;
            }

            if (p.hitByObject)
            {
                arduino.digitalWrite(pinBuzzer, Arduino.HIGH);
            }
            else
            {
                arduino.digitalWrite(pinBuzzer, Arduino.LOW);
            }

            potentiometerValue = arduino.analogRead(pinPotentiometer);
            p.SetVerticalPos(potentiometerValue);
            Debug.Log(potentiometerValue);

            if(p.healthPoints <= 0)
            {
                canvas.SetActive(true);
            }

            // Check if the LED is ready to be activated
            if (LEDboolean) {
                // simple timer
                timePassed += Time.deltaTime;

                // Check if random number is set for the LED to light up
                if (!rndIsSet) {
                    rnd = UnityEngine.Random.Range(2, 7);
                    rndIsSet = true;
                }
                
                // Used to light the LED once within every 15 secs.
                if (timePassed < 15) {
                    // Check if the time passed is more than the rnd number for the LED
                    if (timePassed >= rnd) {
                        // Turn on LED
                        arduino.digitalWrite(pinLed, Arduino.HIGH);

                        // Detect if player activates the touch sensor
                        if(touchValue == 1) {
                            hasTouched = true;
                        } 

                        if (timePassed > rnd + 2) {
                            // Turn off LED since 2 seconds have passed the rnd number
                            arduino.digitalWrite(pinLed, Arduino.LOW);

                            // Used to decrease the players health if he didn't activate the touch sensor with the 2 seconds
                            if (!hasTouched) {
                                p.DecreaseHealthPoints();
                                hasTouched = true;
                            }
                        }
                    }
                } else {
                    // Used to reset everything
                    timePassed = 0;
                    LEDboolean = false;
                    LEDboolean = true;
                    rndIsSet = false;
                    hasTouched = false;
                }
            }

        }

        
        
    }
}
