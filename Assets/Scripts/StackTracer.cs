using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class StackTracer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Console.SetError(new StackTraceTextWriter(GetComponent<TextMeshProUGUI>()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private class StackTraceTextWriter : System.IO.TextWriter
    {
        private TextMeshProUGUI gui;
        public StackTraceTextWriter(TextMeshProUGUI gui)
        {
            this.gui = gui;
        }
        public override Encoding Encoding { get;}

        public override void Write(bool value)
        {
            gui.SetText(gui.text + value);
        }

        public override void Write(char value)
        {
            gui.SetText(gui.text + value);
        }

        public override void Write(char[] buffer) { 
            gui.SetText(gui.text + buffer);
        }
        public override void Write(char[] buffer, int index, int count) { 
            gui.SetText(gui.text + buffer);
        }
        public override void Write(decimal value) {
            gui.SetText(gui.text + value);
        }
        public override void Write(double value) {
            gui.SetText(gui.text + value);
        }
        public override void Write(float value) {
            gui.SetText(gui.text + value);
        }
        public override void Write(int value)
        {
            gui.SetText(gui.text + value);
        }
        public override void Write(long value)
        {
            gui.SetText(gui.text + value);
        }
        public override void Write(object value)
        {
            gui.SetText(gui.text + value);
        }
        public override void Write(ReadOnlySpan<char> buffer)
        {
            gui.SetText(gui.text + buffer.ToString());
        }
        public override void Write(string format, object arg0)
        {
            gui.SetText(gui.text + String.Format(format, arg0));
        }
        public override void Write(string format, object arg0, object arg1)
        {
            gui.SetText(gui.text + String.Format(format, arg0, arg1));
        }
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            gui.SetText(gui.text + String.Format(format, arg0, arg1, arg2));
        }
        public override void Write(string format, object[] args)
        {
            gui.SetText(gui.text + String.Format(format, args));
        }
        public override void Write(string value)
        {
            gui.SetText(gui.text + value);
        }
        public override void Write(uint value)
        {
            gui.SetText(gui.text + value);
        }
        public override void Write(ulong value)
        {
            gui.SetText(gui.text + value);
        }
        public override void WriteLine()
        {
            gui.SetText(gui.text + "\n");
        }
        public override void WriteLine(bool value)
        {
            Write(value+"\n");
        }
        public override void WriteLine(char value)
        {
            Write(value + "\n");
        }
        public override void WriteLine(char[] buffer)
        {
            Write(buffer + "\n");
        }
        public override void WriteLine(char[] buffer, int index, int count)
        {
            Write(buffer + "\n");
        }
        public override void WriteLine(decimal value)
        {
            Write(value + "\n");
        }
        public override void WriteLine(double value)
        {
            Write(value + "\n");
        }
        public override void WriteLine(float value)
        {
            Write(value + "\n");
        }
        public override void WriteLine(int value)
        {
            Write(value + "\n");
        }
        public override void WriteLine(long value)
        {
            Write(value + "\n");
        }
        public override void WriteLine(object value)
        {
            Write(value + "\n");
        }
        public override void WriteLine(ReadOnlySpan<char> buffer)
        {
            Write(buffer.ToString() + "\n");
        }
        public override void WriteLine(string format, object arg0)
        {
            Write(String.Format(format, arg0) + "\n");
        }
        public override void WriteLine(string format, object arg0, object arg1)
        {
            Write(String.Format(format, arg0, arg1) + "\n");
        }
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Write(String.Format(format, arg0, arg1, arg2) + "\n");
        }
        public override void WriteLine(string format, object[] args)
        {
            Write(String.Format(format, args) + "\n");
        }
        public override void WriteLine(string value)
        {
            Write(value + "\n");
        }
        public override void WriteLine(uint value)
        {
            Write(value + "\n");
        }
        public override void WriteLine(ulong value)
        {
            Write(value + "\n");
        }
}
}
