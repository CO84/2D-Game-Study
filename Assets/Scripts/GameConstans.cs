using UnityEngine;

public static class GameConstans
{
    #region Player
    public const string PlayerIdleState = "idle";
    public const string PlayerMoveState = "move";
    public const string YVelocity = "yVelocity";
    public const string JumpFall = "jumpFall";
    public const string WallSlide = "wallSlide";
    public const string Dash = "dash";
    public const string BasicAttack = "basicAttack";
    public const string ComboIndex = "basicAttackIndex";

    public const string JumpAttack = "jumpAttack";
    public const string JumpAttackTrigger = "jumpAttackTrigger";

    #endregion

    #region Enemy
    public const string EnemyIdleState = "idle";
    public const string EnemyMoveState = "move";
    public const string EnemyAttackState = "attack";
    public const string EnemyBattleState = "battle";
    public const string MoveAnimSpeedMultiplier = "moveAnimSpeedMultiplier";
    public const string BattleAnimSpeedMultiplier = "battleAnimSpeedMultiplier";

    public const string XVelocity = "xVelocity";
    #endregion

    //TAGS
    public const string PlayerTag = "Player";
    //LAYERS
    public const string Player = "Player"; // Layer name for the player


    public const int FirstComboIndex = 1; // Starting index for the combo
}
