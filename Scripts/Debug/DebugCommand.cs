using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DebugCommandBase {

    private string _commandID;
    private string _commandDesc;
    private string _commandFormat;

    public string commandID { get { return _commandID; } }
    public string commandDesc { get { return _commandDesc; } }

    public string commandFormat { get { return _commandFormat; } }

    public DebugCommandBase(string id, string description, string format) {

        _commandID = id;
        _commandDesc = description;
        _commandFormat = format;

    }

}

public class DebugCommand : DebugCommandBase {

    private Action command;

    public DebugCommand(string id, string description, string format, Action command) : base (id, description, format) {

        this.command = command;

    }

    public void Invoke() {

        command.Invoke();

    }

}

public class DebugCommand<T1> : DebugCommandBase {

    private Action<T1> command;

    public DebugCommand(string id, string description, string format, Action<T1> command) : base (id, description, format) {

        this.command = command;

    }

    public void Invoke(T1 Value) {

        command.Invoke(Value);

    }

}
