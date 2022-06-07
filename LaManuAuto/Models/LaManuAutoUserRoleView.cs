using LaManuAuto.Areas.Identity.Data;

namespace LaManuAuto.Models;

public class LaManuAutoUserRoleView
{
   public LaManuAutoUser User { get; set; }
    public IEnumerable<string> Roles { get; set; }
}
