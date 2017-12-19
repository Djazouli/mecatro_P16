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
        public readonly ElementSurface surfaceDeControle;
        public readonly PointOriente pointDeReference;
        public readonly double toleranceAngulaire;
        public readonly TypeDeLieu typeDeLieu;
        
        private static Hashtable LIEUX_CLES = new Hashtable();
        private static readonly ElementSurface SURFACE_DE_CONTROLE_STANDARD = new Rectangle(new PointOriente(0,0), 2,2); // A fixer
        private static readonly double TOLERANCE_ANGULAIRE_STANDARD = 5; // A fixer, radian ou degrès ?

        /// <summary>
        /// La surface de contrôle sert à valider la position du robot : cf IsAtTheRightPlace
        /// Attention : la surface doit avoir pour origine (0,0), elle est clonée puis translatée automatiquement.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pt"></param>
        /// <param name="surfaceControle"></param>
        /// <param name="toleranceAngulaire"></param>
        public LieuCle(TypeDeLieu type, PointOriente pt, ElementSurface surfaceControle, double toleranceAngulaire) {
            this.surfaceDeControle = surfaceControle.clone();
            this.pointDeReference = pt;
            this.toleranceAngulaire = System.Math.Abs(toleranceAngulaire);
            this.typeDeLieu = type;

            this.surfaceDeControle.translater(pt);
        }


        public static void LoadAllLieuxCles()
        {
            LIEUX_CLES.Clear();

            // AJOUT DES LIEUX CLES ICI

            // FIN DE LAJOUT DES LIEUX CLES

        }

        public Boolean IsAtTheRightPlace(PointOriente pt)
        {
            if (System.Math.Abs(pt.theta - pointDeReference.theta) > toleranceAngulaire)
                return false;
            if(!surfaceDeControle.Appartient(pt))
                return false;
            return true;
        }

        public static LieuCle buildAndAddLieuCle(TypeDeLieu type, CouleurEquipe couleur, PointOriente pt, ElementSurface surfaceControle, double toleranceAngulaire) {
            LieuCle retour = new LieuCle(type, pt, surfaceControle, toleranceAngulaire);
            addLieuCle(type, couleur, retour);
            return retour;
        }

        private static void addLieuCle(TypeDeLieu type, CouleurEquipe couleur, LieuCle lieu) {
            if(!LIEUX_CLES.Contains(couleur)) {
                LIEUX_CLES.Add(couleur, new Hashtable());
            }
            ((Hashtable)LIEUX_CLES[couleur]).Add(type, lieu);
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
