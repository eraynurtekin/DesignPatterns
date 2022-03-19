using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Command.Commands
{
    public class FileCreateInvoker
    {
        private ITableActionCommand _command;
        private List<ITableActionCommand> tableCommands = new List<ITableActionCommand>();

        public void SetCommand(ITableActionCommand tableCommand)
        {
            _command = tableCommand;
        }
        public void AddCommand(ITableActionCommand tableCommand)
        {
            tableCommands.Add(tableCommand);
        }

        public IActionResult CreateFile()
        {
            return _command.Execute();
        }
        public List<IActionResult> CreateFiles()
        {
            
            return tableCommands.Select(x=>x.Execute()).ToList();
        }

    }
}
