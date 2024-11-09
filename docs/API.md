# API Docs

## Overview

- 이 API는 사용자 인증 및 일기 관리 기능을 제공
- 구글 OAuth 2.0을 사용하여 인증을 처리하며, 각 엔드포인트는 인증된 사용자만 접근할 수 있음
- 주요 기능: 사용자 로그인, 특정 날짜의 일기 조회, 새로운 일기 작성, 일기 수정 및 삭제

### 1. 구글 로그인 API

- 설명: 사용자가 로그인할 때 호출되며, 사용자 인증을 처리함
- 메서드: POST
- 엔드포인트: /api/google-login
- Request : { "username": "사용자 이름", "password":"비밀번호"}
- Response :
  - 성공 시 (200) : {"accessToken" : "acessToken 토큰" , "message": "로그인 성공"}
  - 실패 시 (401) : {"message": "로그인 실패"}

### 2. 일기 조회 API

- 설명: 특정 날짜의 일기 데이터 조회
- 메서드: GET
- 엔드포인트: /entries
- Request 헤더 : Authorization - accessToken
- Request 파라미터 : date - 조회할 날짜
- Response :
  - 성공 시 (200) : {"entry": {"date" : "일기날짜" ,  "entryId": "일기 고유 ID", "content": "해당 날짜의 일기 내용"} }
  - 실패 시 (401) : {"message": "일기없음"}

### 3. 일기 작성 API

- 설명: 새로운 일기 작성
- 메서드: POST
- 엔드포인트: /entries
- Request 헤더 : Authorization - accessToken
- Request 바디 : { "date": "해당 날짜", "content": "새로 작성할 일기 내용"}
- Response :
  - 성공 시 (201) : {"message": "일기 작성 완료"}
  - 실패 시 (400) : {"message": "일기 작성 실패"}

### 4. 일기 수정 API

- 설명: 기존 일기 내용 수정
- 메서드: PUT
- 엔드포인트: /entries/{entryId}
- Request 헤더 : Authorization - accessToken
- Request 바디 : { "content": "수정된 일기 내용"}
- Response :
  - 성공 시 (200) : {"message": "일기 수정 완료"}
  - 실패 시 (404) : {"message": "일기 수정 실패"}

### 5. 일기 삭제 API

- 설명: 특정 일기 삭제
- 메서드: DELETE
- 엔드포인트: /entries/{entryId}
- Request 헤더 : Authorization - accessToken
- Response :
  - 성공 시 (200) : {"message": "일기 삭제 완료"}
  - 실패 시 (404) : {"message": "일기 삭제 실패"}
