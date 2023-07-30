using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;  // Prefab des Geschosses

    [SerializeField]
    private float _bulletSpeed;  // Geschwindigkeit des Geschosses

    [SerializeField]
    private Transform _gunOffset;  // Die Position, an der das Geschoss erstellt werden soll

    [SerializeField]
    private float _timeBetweenShots;  // Zeitlicher Abstand zwischen den Sch�ssen

    private bool _fireContinuously;  // Gibt an, ob das Schie�en kontinuierlich erfolgt
    private bool _fireSingle;  // Gibt an, ob ein einzelner Schuss abgegeben wird
    private float _lastFireTime;  // Zeitpunkt des letzten Schusses

    void Update()
    {
        if (_fireContinuously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            // �berpr�fe, ob der zeitliche Abstand zwischen den Sch�ssen erreicht ist
            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet();  // Schie�e das Geschoss ab

                _lastFireTime = Time.time;
                _fireSingle = false;  // Setze das Flag f�r den einzelnen Schuss zur�ck
            }
        }
    }

    private void FireBullet()
    {
        // Erstelle ein neues Geschoss an der Position des Spielers in Richtung des Spielers
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);

        // Erhalte die Rigidbody2D-Komponente des Geschosses
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        // Berechne die Geschwindigkeit des Geschosses entlang der Y-Achse des Spielers
        rigidbody.velocity = _bulletSpeed * transform.up;

    }

    private void OnFire(InputValue inputValue)
    {
        _fireContinuously = inputValue.isPressed;  // Aktiviere kontinuierliches Schie�en, wenn der Schussknopf gedr�ckt wird

        if (inputValue.isPressed)
        {
            _fireSingle = true;  // Aktiviere einzelnen Schuss, wenn der Schussknopf gedr�ckt wird
        }
    }
}
