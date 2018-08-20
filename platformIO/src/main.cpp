#include <Arduino.h>
#include <WiFi.h>
#include <WiFiClientSecure.h>
#include "TelegramBot.h"
#include "secrets.h"

#define LED 13
#define DOOR_BELL 23

const char *fcmServer = "fcm-xmpp.googleapis.com";

WiFiClientSecure client;
const char *token = BOT_TOKEN;
TelegramBot BotTelegram(token, client);

bool IsRingin();
void SendAlarmToFirebase();

void setup()
{
    pinMode(LED, OUTPUT);
    pinMode(DOOR_BELL, INPUT);
    Serial.begin(9600);
    Serial.println("Start door_bell_visualizer 0.1");
    Serial.print("Connect to ");
    Serial.print(SSID);
    WiFi.begin(SSID, WIFI_PASS);

    while (WiFi.status() != WL_CONNECTED)
    {
        delay(100);
        Serial.print(".");
    }
    BotTelegram.begin();
    
    Serial.println("done");
    Serial.println(WiFi.localIP());
    Serial.println(WiFi.dnsIP());

    delay(100);
    BotTelegram.sendMessage(CHAT_ID, "Es klingelnt dingelt an der Tür");
    //Serial.println(client.connect("www.google.de",443));

    //Serial.println(WiFi.hostName("www.google.de"));
    // put your setup code here, to run once:
}

unsigned long lastRing;

void loop()
{

    // int read =
    //Serial.println(read);
    if (IsRingin())
    {
        if (millis() - lastRing > 10000)
        {

            lastRing = millis();
            BotTelegram.sendMessage(CHAT_ID, "Es klingelnt dingelt an der Tür");
        }
    }
    digitalWrite(LED, IsRingin());
    //delay(10);
    // put your main code here, to run repeatedly:
}

bool IsRingin()
{
    uint time = millis();
    int a = 0;
    while (millis() - time < 20)
    {
        if (digitalRead(DOOR_BELL))
            a += 3;
        else
            a -= 1;
        delay(1);
    }
    //Serial.println(a);// 20 wenns klingelt, 60 wenns nicht klingelt
    return a < 40;
}

void SendAlarmToFirebase()
{
    String data = "{";
    data = data + "\"to\": \"your_smartphone_id\",";
    data = data + "\"notification\": {";
    data = data + "\"body\": \"Motion detected\",";
    data = data + "\"title\" : \"Alarm\" ";
    data = data + "} }";
    Serial.println("Send data...");
    if (client.connect(fcmServer, 80))
    {
        Serial.println("Connected to the server..");
        client.println("POST /fcm/send HTTP/1.1");
        client.println("Authorization: key=auth_key");
        client.println("Content-Type: application/json");
        client.println("Host: fcm.googleapis.com");
        client.print("Content-Length: ");
        client.println(data.length());
        client.print("\n");
        client.print(data);
    }
    Serial.println("Data sent...Reading response..");
    while (client.available())
    {
        char c = client.read();
        Serial.print(c);
    }
    Serial.println("Finished!");
    client.flush();
    client.stop();
}