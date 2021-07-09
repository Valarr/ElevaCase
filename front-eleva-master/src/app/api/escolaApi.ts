import {axiosInstance} from './base'

interface escolaProps{nomeEscola:string, enderecoEscola:string, telefoneEscola:string}

export default class School {

    static async criaEscola (attributes: any) {
        console.log(attributes, 'cria escola c=====')
        const response = await axiosInstance.post('/Escola', attributes);
        return response.data;
    }

    static async fetchAll () {
        const response = await axiosInstance.get('/Escola');
        return response.data;
    }

    static async fetchById (id: any) {
        const response = await axiosInstance.get(`/Escola/${id}`);
        return response.data;
    }

    static async update (id: string, attributes: any) {
        const response = await axiosInstance.put(`/Escola/${id}`, attributes);
        return response.data;
    }

    static async deleteById (id: string) {
        const response = await axiosInstance.delete(`/Escola/${id}`);
        return response.data;
    }
}