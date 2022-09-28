using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Set In Inspector")]
    // число секунд полного цикла синусоиды
    public float waveFrequency = 4;
    // ширина синусоиды в метрах
    public float waveWidth = 4;
    public float waveRotX = 45;

    private BoundsCheck bndCheck;
    private Renderer rend;
    private float birthTime;

    [Header("Set Dynamically")]
    public Rigidbody rigid;
    public float x0;

    [SerializeField]
    private WeaponType _type;
    // Это общедоступное свойство маскирует поле _type и обрабатывает операции присваивания ему нового значения
    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        birthTime = Time.time;
        x0 = transform.position.x;
    }
    void Update()
    {
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }
        Move();
    }
    /// <summary>
    /// Изменяет скрытое поле _type и устанавливает цвет этого снаряда, как определено в WeaponDefinition.
    /// </summary>
    /// <param name="eType"> Тип WeaponType используемого оружия. </param>
    public void SetType(WeaponType eType)
    {
        // Установить _type
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;
    }
    public void Move()
    {
        switch(type)
        {
            case (WeaponType.phaser):
                Vector3 tempPos = transform.position;
                float age = Time.time - birthTime;
                float theta = Mathf.PI * 2 * age * waveFrequency;
                float sin = Mathf.Sin(theta);
                float cos = -Mathf.Cos(theta);
                tempPos.x = waveWidth * sin + x0;
                transform.position = tempPos;
                //sin = Mathf.Sin(sin * Mathf.PI);
                Vector3 rot = new Vector3(0, 0, cos * waveRotX);
                this.transform.rotation = Quaternion.Euler(rot);
                break;
        }
    }
}
