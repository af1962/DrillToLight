# DrillToLight
J'ai conçu ce petit programme sans prétention pour convertir du gcode prévu pour du fraisage vers un usinage laser.
Je m'en sers exclusivement pour la sérigraphie de mes PCB.
La conversion est automatique mais vous pouvez choisir un point de départ si vous le souhaitez.
Pour information, l'algoritme de base recherche la première ligne avec "G00" et la dernière avec "Zxx".
Tous les Z0 sont transformés en S100 (10% puissance) et tous les Z>0 en S0 (arrêt du laser). La vitesse est de 150. Il s'agit là de valeurs par défaut et qui permettent de sérigrafier un PCB simple face (préalablement peint en blanc) avec un laser de 4 Watts.
