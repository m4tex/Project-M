using System;
using System.Collections.Generic;
using TEST_ZONE;
using Unity.Collections;

public class Magazine
{
    private int capacity;
    private int currentAmmo;
    private Queue<ParabolicBullet.BulletType> bullets;

    public Magazine(int capacity, ParabolicBullet.BulletType bulletType, ParabolicBullet.BulletType[] bullets = null)
    {
        this.capacity = capacity;
        currentAmmo = capacity;

        if (bullets == null && bulletType != null)
        {
            for (int i = 0; i < capacity; i++)
            {
                this.bullets.Enqueue(bulletType);
            }
        }
        else
        {
            int iterations = bullets.Length < capacity ? bullets.Length : capacity;
            for (int i = 0; i < iterations; i++)
            {
                this.bullets.Enqueue(bullets[i]);
            }
        }
    }

    public ParabolicBullet.BulletType NextBulletType()
    {
        return bullets.Dequeue();
    }
}