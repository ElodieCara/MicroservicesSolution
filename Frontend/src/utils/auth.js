export const getAuthHeaders = () => {
    const token = localStorage.getItem("jwt"); // Récupérer le token stocké
    if (!token) {
        throw new Error("Utilisateur non connecté."); // Lever une erreur si le token n'existe pas
    }
    return {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`, // Ajouter le token au header
    };
};
