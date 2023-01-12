namespace MIIProjekt.Player.States
{
    public enum PlayerTransition
    {
        Invalid,
        JumpingFinished,
        PlayerOnGround,
        Jumped,
        PlayerNotOnGround,
        CoyoteTimeFinished,
        Died,
        Finish,
    }
}
