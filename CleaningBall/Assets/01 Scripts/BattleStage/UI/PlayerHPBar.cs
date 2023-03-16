public class PlayerHPBar : HPBar
{

    private void Start()
    {
        if (TheTarget == null)
            Destroy(gameObject);
    }
    private void Update()
    {
        UpdateGauge();
    }
}
