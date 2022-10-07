clc;
clear all;
close all;

myMQTT = mqtt('tcp://192.168.1.17:1883');

Topic = "SM_SHE";
%Message = '1';

%Publish(myMQTT, Topic, Message);

Data = subscribe(myMQTT, Topic, 'Callback', @myMQTT_Callback);

function myMQTT_Callback(topic, msg)
disp(topic)
disp(msg);
end

