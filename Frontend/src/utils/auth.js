export const getAuthHeaders = () => {
    const token = localStorage.getItem("jwt"); // R�cup�rer le token stock�
    if (!token) {
        throw new Error("Utilisateur non connect�."); // Lever une erreur si le token n'existe pas
    }
    return {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`, // Ajouter le token au header
    };
};
