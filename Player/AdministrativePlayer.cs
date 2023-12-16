public class AdministrativePlayer : Player
{
    public AdministrativePlayer(string name) : base(name)
    {
        Stats.SetHealth(100);
        Stats.SetSpeed(3.5f);
        Stats.SetStamina(60);
        // Initialize with Administrative specific items like high-level access, communication devices
    }

    public override void Move() { /* Movement logic */ }
    public override void Attack() { /* Attack logic */ }
    public override void Interact() { /* Interaction logic */ }
    public override void PerformRoleSpecificAction() { }
}
