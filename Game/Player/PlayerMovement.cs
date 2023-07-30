using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;  // Die Geschwindigkeit des Spielers

    [SerializeField]
    private float _rotationSpeed;  // Die Rotationsgeschwindigkeit des Spielers

    [SerializeField]
    private float _screenBorder;  // Der Abstand des Spielers zum Bildschirmrand

    private Rigidbody2D _rigidbody;  // Die Rigidbody2D-Komponente des Spielers
    private Vector2 _movementInput;  // Die Eingabe für die Spielerbewegung
    private Vector2 _smoothedMovementInput;  // Die geglättete Eingabe für die Spielerbewegung
    private Vector2 _movementInputSmoothVelocity;  // Die Geschwindigkeit der Eingabeglättung
    private Camera _camera;  // Die Hauptkamera des Spiels

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();  // Zugriff auf die Rigidbody2D-Komponente des Spielers
        _camera = Camera.main;  // Zugriff auf die Hauptkamera des Spiels

}

private void FixedUpdate()
    {
        SetPlayerVelocity();  // Setzt die Geschwindigkeit des Spielers
        RotateInDirectionOfInput();  // Rotiert den Spieler in Richtung der Eingabe
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
                    _smoothedMovementInput,
                    _movementInput,
                    ref _movementInputSmoothVelocity,
                    0.1f);  // Glättet die Spielerbewegungseingabe

        _rigidbody.velocity = _smoothedMovementInput * _speed;  // Setzt die Geschwindigkeit des Spielers basierend auf der Eingabe

        PreventPlayerGoingOffScreen();  // Verhindert, dass der Spieler den Bildschirmrand überschreitet
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);  // Konvertiert die Spielerposition in Bildschirmkoordinaten

        if ((screenPosition.x < _screenBorder && _rigidbody.velocity.x < 0) ||
            (screenPosition.x > _camera.pixelWidth - _screenBorder && _rigidbody.velocity.x > 0))
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);  // Stoppt die horizontale Bewegung des Spielers, wenn er den Bildschirmrand erreicht
        }

        if ((screenPosition.y < _screenBorder && _rigidbody.velocity.y < 0) ||
            (screenPosition.y > _camera.pixelHeight - _screenBorder && _rigidbody.velocity.y > 0))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);  // Stoppt die vertikale Bewegung des Spielers, wenn er den Bildschirmrand erreicht
        }
    }

    private void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);  // Berechnet die Zielrotation basierend auf der Eingabe
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);  // Interpoliert die aktuelle Rotation des Spielers zur Zielrotation

            _rigidbody.MoveRotation(rotation);  // Aktualisiert die Rotation des Spielers
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();  // Aktualisiert die Spielerbewegungseingabe basierend auf der Eingabe des Spielers
    }
}
