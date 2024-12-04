using UnityEngine;

public class InvertMegaManCommand : ICommand
{
    private MegaMan megaMan;

    public InvertMegaManCommand(MegaMan megaMan)
    {
        this.megaMan = megaMan;
    }

    public void Execute()
    {
        Vector3 scale = megaMan.transform.localScale;
        scale.y = -scale.y;
        megaMan.transform.localScale = scale;
        megaMan.canJump = !megaMan.canJump;
    }

    public void Undo()
    {
        Execute();
    }
}
