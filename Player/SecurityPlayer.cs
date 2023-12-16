public class SecurityPlayer : Player
{
    public SecurityPlayer(string name) : base(name)
    {
        Stats.SetHealth(150);
        Stats.SetSpeed(4.5f);
        Stats.SetStamina(70);
        // Initialize with Security specific equipment like weapons, armor
    }

    public override void Move() { /* Movement logic */ }
    public override void Attack() { /* Attack logic */ }
    public override void Interact() { /* Interaction logic */ }
    public override void PerformRoleSpecificAction() { }
}
