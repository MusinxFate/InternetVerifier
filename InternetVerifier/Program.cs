// See https://aka.ms/new-console-template for more information

using System.Net.NetworkInformation;
using NAudio;
using NAudio.Wave;

var ping = new Ping();
var noConnection = true;
Console.WriteLine("Please enter the file path of the file you wish to be played:");

while (noConnection)
{
    PingReply pingreply = ping.Send("google.com");
    if (pingreply.Status == IPStatus.Success)
    {
        PlayAudio();
        noConnection = false;
    }
}

void PlayAudio()
{
    var audioFilePath = Console.ReadLine();
    using (var audioFile = new AudioFileReader(audioFilePath))
    using (var outputDevice = new WaveOutEvent())
    {
        outputDevice.Init(audioFile);
        outputDevice.Play();
        while (outputDevice.PlaybackState == PlaybackState.Playing)
        {
            Thread.Sleep(6000);
            outputDevice.Stop();
        }
    }
}