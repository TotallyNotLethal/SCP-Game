public class ScientistPlayer : Player
{
    public ScientistPlayer(string name) : base(name)
    {
        Stats.SetHealth(80);
        Stats.SetSpeed(4.0f);
        Stats.SetStamina(40);
        // Initialize with Scientist specific equipment like research tools, access cards
    }

    public override void Move() { /* Movement logic */ }
    public override void Attack() { /* Attack logic */ }
    public override void Interact() { /* Interaction logic */ }
    public override void PerformRoleSpecificAction() { }
}
