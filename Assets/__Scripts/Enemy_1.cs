using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy_1 расшир€ет класс Enemy
public class Enemy_1 : Enemy
{
    [Header("Set in Inspector: Enemy_l")]
    // число секунд полного цикла синусоиды
    public float waveFrequency = 2;
    // ширина синусоиды в метрах
    public float waveWidth = 4;
    public float waveRotY = 45;
    private float x0; // Ќачальное значение координаты X
    private float birthTime;
    // ћетод Start хорошо подходит дл€ наших целей,
    // потому что не используетс€ суперклассом Enemy
    void Start()
    {
        // ”становить начальную координату X объекта Enemy_l
        x0 = pos.x;
        birthTime = Time.time;
    }
    // ѕереопределить функцию Move суперкласса Enemy
    public override void Move()
    {
        // “ак как pos - это свойство, нельз€ напр€мую изменить pos.x поэтому получим pos в виде вектора Vector3, доступного дл€ изменени€
        Vector3 tempPos = pos;
        // значение theta измен€етс€ с течением времени
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth * sin;
        pos = tempPos;
        // повернуть немного относительно оси Y
        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);
        // base.Move() обрабатывает движение вниз, вдоль оси Y
        base.Move();
        // print( bndCheck.isOnScreen );
    }
}
