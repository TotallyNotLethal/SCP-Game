public class DClassPlayer : Player
{
    public DClassPlayer(string name) : base(name)
    {
        Stats.SetHealth(100);
        Stats.SetSpeed(5.0f);
        Stats.SetStamina(50);
        // Initialize with D-Class specific equipment if any
    }

    public override void Move() { /* Movement logic */ }
    public override void Attack() { /* Attack logic */ }
    public override void Interact() { /* Interaction logic */ }
    public override void PerformRoleSpecificAction() { }
}
