using UnityEngine;

public class g_refs : ut_singleton<g_refs>
{
    public pd_session sessionData;
    public pl_health_manager plHealth;
    public Transform plTrans;
    public Rigidbody plRB;
    public GameManager gameManager;
    public lv_pool pool;
    public wv_manager waveManager;
    public sfx_oneshot2d sfxOneshot2D;
}
