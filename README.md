# AA3_Fonaments_Modelatge

## Scripts

### FABRIK

Script que implementa el algoritmo de FABRIK, usando Forward, Backward y volviendo a la posición inicial una vez se consigue llegar al dron.

### CCD

Script que implementa el algoritmo de CCD, al igual que en el del FABRIK, el brazo vuelve a su posición inicial al obtener el dron.

### Drone Movement

Simple Script para mover el dron de forma automatica.

## Comparativa

A simple vista, FABRIK y CCD aportan resultados muy distintos entre los 2, sobretodo en la suavidad del movimiento. Con FABRIK los movimientos son super smooth y para esta simulación en gravedad 0 parece que el brazo de verdad esté en el espacio, mientras que CCD nos da un resultado muy clunky, sobretodo cuando el dron está fuera de su alcance, momento en el que el brazo se pone a vibrar sin saber muy bien qué hacer. De media FABRIK ha necesitado 2100 iteraciones para llegar al dron pero CCD ha llegado varias veces a su límite si el dron quedaba mucho rato fuera de su alcance.

En conclusión, FABRIK da un resultado más atractivo y fiel a la simulación mientras que CCD da varios problemas que hacen que no sea un buen modelo para usarse en este contexto.