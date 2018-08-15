#include <Arduino.h>


#define LED 13
#define DOOR_BELL 23


bool IsRingin();
void setup() {
    pinMode(LED,OUTPUT);
    pinMode(DOOR_BELL,INPUT);
    Serial.begin(9600);

    // put your setup code here, to run once:
}

void loop() {
   // int read = 
    //Serial.println(read);
    digitalWrite(LED,IsRingin());
    //delay(10);
    // put your main code here, to run repeatedly:
}


bool IsRingin(){
    int time = millis();
    int a = 0;
    while (millis() - time < 20)
    {
        if (digitalRead(DOOR_BELL))
            a+=3;
        else
            a-=1;
        delay(1);
    }
    //Serial.println(a);// 20 wenns klingelt, 60 wenns nicht klingelt
    return a < 40;
}