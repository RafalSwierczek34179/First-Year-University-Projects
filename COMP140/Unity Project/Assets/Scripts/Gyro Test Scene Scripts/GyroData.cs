using UnityEngine;
using System.Diagnostics;

public class GyroData : MonoBehaviour
{
    public float x, y, z = 0f;
    private string error = "";
    private string result = "";
    private void Start()
    {
        var psi = new ProcessStartInfo();
        //psi.FileName = @"/usr/bin/python3.exe";
        psi.FileName = @"C:\Program Files\Python39\python.exe";

        var script = @"C:\#Personal\GyroTestPython\gyroTestPython.py";
        //var script = @"/home/pi/mpu_6050.py";
        psi.Arguments = $"\"{script}\"";

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        using(var process = Process.Start(psi))
        {
            error = process.StandardError.ReadToEnd();
            result = process.StandardOutput.ReadToEnd();
        }
    }
    private void Update()
    {
        
    }
    public string GetError()
    {
        return error;
    }
    public string GetResult()
    {
        return result;
    }
}
