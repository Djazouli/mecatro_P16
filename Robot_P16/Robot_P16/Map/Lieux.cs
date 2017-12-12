using System;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Robot;
using Robot_P16.Map.Surface;

namespace Robot_P16.Map
{
    /// <summary>
    /// TypeDeLieu est une enum qui permet de récupérer un lieu clé en donnant le TypeDeLieu et la couleur de l'équipe.
    /// </summary>
    public enum TypeDeLieu
    {
        POINT_DEPART_GR,
        POINT_DEPART_PR,

        CUBES_1,
        CUBES_2, // ...

        INTERRUPTEUR,
        BASSIN_RECUPERATEUR //...
    }
    class LieuCle
    {
        ElementSurface surfaceDeControle;
        PointOriente pointDeReference;
        double toleranceAngulaire;
        
        private static Hashtable LIEUX_CLES = new Hashtable();

        private static void addLieuCle(TypeDeLieu type, CouleurEquipe couleur, LieuCle lieu) {
            if(!LIEUX_CLES.Contains(couleur)) {
                LIEUX_CLES.Add(couleur, new Hashtable());
            }
            ((Hashtable)LIEUX_CLES[couleur]).Add(type, lieu);
        }

        public static void LoadAllLieuxCles() {
            LIEUX_CLES.Clear();
            
            // AJOUT DES LIEUX CLES ICI

            // FIN DE LAJOUT DES LIEUX CLES
            
        }

        public Boolean IsAtTheRightPlace(PointOriente pt) {
            return false;
        }
        public static LieuCle GetLieuCleFor(TypeDeLieu type, CouleurEquipe couleur) {
            if(LIEUX_CLES.Contains(couleur)) {
                Hashtable lieux_associes = (Hashtable)LIEUX_CLES[couleur];
                if(lieux_associes.Contains(type))
                    return (LieuCle)lieux_associes[type];
            }
            return null;
        }

    }
}
