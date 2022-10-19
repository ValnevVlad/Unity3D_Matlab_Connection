clc;
clear all;
close all;

myMQTT = mqtt('tcp://192.168.1.17:1883');

Topic = "SM_SHE/write";
%Message = '{"id": "Channe2.SmLink2_MAIN.Main4_Con2", "v": "3"}';
Message = '[{"id": :Channe2.SmLink2_MAIN.Main4_Con2, "v": "3"}]';

Publish(myMQTT, Topic, Message, 'Qos', 0);