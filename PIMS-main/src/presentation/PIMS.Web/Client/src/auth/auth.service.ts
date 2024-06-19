import api from '@/api/api-base-hub'
import type { AxiosResponse } from 'axios'
import * as jose from 'jose'
export enum ChallengeResponseType {
  JWT = 'JWT',
  AD = 'AD'
}
export type ChallengeResponse = {
  AuthType: ChallengeResponseType
}
export type AuthenticationResponse = {
  UserName: string
  FirstName: string
  MiddleName: string
  LastName: string
  Email: string
  Token: string
}
export type FormsLoginRequest = {
  UserName: string
  Password: string
}
export type User = {
  UserName: string
  
}
export type SearchResults = {
  searchResults: string
  
}
export type FormsRegisterRequest = {
  UserName: string
  Password: string
  FirstName: string
  MiddleName: string
  LastName: string
  Email: string
}
const key = 'token'
export async function challengeAuthFromServer(): Promise<AxiosResponse<ChallengeResponse>> {
  return await api.get<ChallengeResponse>('ChallengeAuthenticate/challenge')
}
export async function UserModel(): Promise<AxiosResponse<User>> {
  return await api.get<User>('HomeController/GetUserViewForClient')
}
export async function authFromAD(): Promise<AxiosResponse<AuthenticationResponse>> {
  return await api.get<AuthenticationResponse>('ADAuthentication/login')
}
export async function authFromForms(
  login: FormsLoginRequest
): Promise<AxiosResponse<AuthenticationResponse>> {
  return await api.post<AuthenticationResponse>('FormsAuthentication/login', login)
}
export async function registerFromForms(
  register: FormsRegisterRequest
): Promise<AxiosResponse<AuthenticationResponse>> {
  return await api.post<AuthenticationResponse>('FormsAuthentication/register', register)
  
}
export async function searchpdf() {
  return await api.get<SearchResults>('PdfController/search-pdf')
}

export function getToken() {
  return localStorage.getItem(key)
}

export function logOut() {
  localStorage.clear()
  window.location.href = '/login'
}
export function isTokenFromLocalStorageValid() {
  const token = localStorage.getItem(key)
  if (!token) {
    return false
  }

  const decoded = jose.decodeJwt(token) as any
  const expiresAt = decoded.exp * 1000
  const dateNow = Date.now()

  return dateNow <= expiresAt
}
export function getUserEmailFromToken() {
  const token = localStorage.getItem(key)
  if (!token) return false

  const decoded = jose.decodeJwt(token) as any

  return decoded.email
}
