# 🔶 Hexagonia

Juego multijugador en 3D hecho con **Unity** y **Photon PUN 2**, inspirado en los party games de
carreras de obstáculos: varios jugadores compiten en la misma partida para llegar a la meta de
cada nivel esquivando obstáculos, suelos que se rompen y lava.

## Cómo se juega

1. En el menú principal escribes tu nombre y pulsas conectar.
2. El juego te une a una sala libre (o crea una nueva, de hasta **10 jugadores**).
3. Tras una cuenta atrás ("LA PARTIDA EMPIEZA… ¡A JUGAR!"), todos salen a la vez.
4. Hay que llegar a la **meta**. Cuando todos los jugadores la alcanzan, se pasa al siguiente nivel.
5. Si caes a la lava o el suelo se rompe bajo tus pies, quedas eliminado.

**Controles:** `WASD` / flechas para moverte y `Espacio` para saltar.

## Funcionalidades

- **Multijugador online** con Photon PUN 2: salas automáticas, nombre de cada jugador sobre su
  personaje (sincronizado por RPC) y cambio de nivel sincronizado para toda la sala.
- **Niveles encadenados**: `SceneMultiplayer1` → `escena2` → `escena3` → `escena4`.
- **Obstáculos**: plataformas y puertas giratorias, obstáculos móviles y suelos que se rompen
  cada vez más rápido según avanza la partida.
- **Muerte en la lava** con pantalla de eliminación y sonido.
- Personajes animados (andar, correr y saltar) y cámara que sigue al jugador.

## Estructura

```
Assets/
├── Scenes/                         Menú, niveles y escena final
├── Scripts/                        Código del juego
│   ├── Menu/                       Menú principal y conexión a Photon
│   ├── CanvasJuego/                Interfaz durante la partida
│   ├── Scripts Nieves y Alejandro/ Inicio de partida, muerte en lava, suelo que se rompe
│   ├── Launcher.cs                 Conexión, sala y aparición de jugadores
│   ├── Meta.cs                     Llegada a la meta y paso de nivel
│   └── Movimiento.cs               Movimiento, salto y animaciones del jugador
├── Prefab/                         Prefabs del juego (suelo hexagonal, modelos propios)
├── Materials/                      Materiales del juego
└── TextMesh Pro/                   Recursos de TextMesh Pro
```

Los paquetes de terceros (Photon, personajes, shaders y modelos) **no están en el repositorio**:
su licencia no permite redistribuirlos. Hay que importarlos antes de abrir el juego (ver abajo).

## Abrir el proyecto

1. Instala **Unity 2022.3.23f1** desde Unity Hub (otra versión obligaría a convertir el proyecto).
2. En Unity Hub: *Add → Add project from disk* y elige la carpeta del repositorio.
   La primera vez Unity tardará unos minutos en regenerar la carpeta `Library/`.
3. Importa desde la **Unity Asset Store** (*Window → Package Manager → My Assets*) los paquetes
   que usa el juego:

   | Paquete | Autor | Para qué se usa |
   |---|---|---|
   | **PUN 2 - FREE** | Exit Games | Multijugador online |
   | **Party Monster Duo** | Dungeon Mason | Personajes de los jugadores |
   | **Lava Flowing Shader** | Moonflower Carnivore | Efecto de la lava |
   | **Surface** | Alvin Sartor | Modelos y piezas de los escenarios |

   Al importarlos con sus rutas por defecto, las escenas recuperan solas sus referencias.
4. Crea un **App ID de Photon PUN** gratuito en [photonengine.com](https://dashboard.photonengine.com)
   (hasta 20 jugadores simultáneos) y pégalo en *Window → Photon Unity Networking → PUN Wizard*.
5. Abre `Assets/Scenes/MainMenu.unity` y pulsa **Play**.

La música y la imagen de eliminación que se usaron durante el desarrollo no se incluyen por
derechos de autor; las escenas funcionan sin ellas y puedes asignar las tuyas en el Inspector.

Las escenas del juego ya están configuradas en *File → Build Settings* (`MainMenu`,
`SceneMultiplayer1`, `escena2`, `escena3` y `escena4`); para generar un ejecutable basta con
pulsar **Build**. `Final` y `Hexagonia` aparecen desactivadas porque ningún nivel las carga.

## Créditos

- Proyecto en equipo. Repositorio de [ZaperMaster](https://github.com/ZaperMaster).
- Multijugador con [Photon PUN 2](https://www.photonengine.com/pun) de Exit Games.
- Personajes, shaders y modelos de los autores indicados en la tabla de arriba, a través de la
  Unity Asset Store.
