using qua3osu;

ConvertCommand command = new();
ConvertAction action = new(command);
command.SetAction(action.Convert);
command.Parse(args).Invoke();