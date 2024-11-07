# 데이터베이스 선택: Redis vs MongoDB

## Redis
![pngwing com (2)](https://github.com/user-attachments/assets/c156b0e1-086b-4c7e-bebf-32acab43d25f)

- **데이터베이스 유형**: NoSQL, 비정형
- **데이터 저장 방식**: RAM에 데이터를 저장하여 메모리에서 직접 접근이 가능하고 응답 지연 시간을 줄여줍니다. 그러나 이로 인해 저장할 수 있는 데이터 양이 제한적이며 메모리에 부담이 갈 수 있습니다.
- **영속성**: Append Only File (AOF) 로깅을 통해 데이터 세트를 영구 저장할 수 있습니다.
- **데이터 구조**: 키-값 페어 형태로 저장합니다.
- **확장성**: MongoDB에 비해 확장성이 낮습니다.
- **사용 사례**: 쿼리 속도가 빠른 임시 데이터 저장소로 추천됩니다.

### Redis 선택 및 전환 이유
초기에 Redis를 주요 데이터베이스로 선택했으나, Redis는 텍스트 기반 데이터에 적합하며 이미지 저장에는 적합하지 않아서 MongoDB로 전환하게 되었습니다.

## MongoDB
![pngwing com (1)](https://github.com/user-attachments/assets/bff5c8d4-6f94-416a-9029-07fd17cea980)

- **데이터베이스 유형**: NoSQL, 비정형
- **데이터 저장 방식**: 문서 데이터 모델에 따라 외부 메모리 스토리지에 데이터를 저장합니다.
- **데이터 형식**: 바이너리 JSON 형태로 직렬화하여 저장합니다.
- **확장성**: 수평적 확장이 가능하여 대용량 데이터 처리에 효율적입니다.
- **영속성**: 주로 디스크 스토리지에 의존하여, 디스크 공간에 따라 기능을 강화할 수 있습니다.
- **인덱싱**: 보조 인덱싱을 지원하여 더 빠른 쿼리와 검색이 가능합니다.
- **가용성**: 가용성이 뛰어나며 대규모 애플리케이션에 적합합니다.

# Database Structure

## Database: MongoDB
- MongoDB를 사용하여 데이터 영구 저장 및 관리.
- 주 컬렉션은 User와 Diary로 나누어짐.

## Collections

### 1. User Collection
- 사용자가 Google 로그인을 통해 인증을 받으면 사용자 정보를 저장.
- 필수 필드는 다음과 같음:

| Field          | Type   | Description                           |
|----------------|--------|---------------------------------------|
| `_id`          | ObjectId | MongoDB 고유 ID                    |
| `googleToken`  | String | Google 로그인 토큰 (고유 식별 값으로 활용 가능) |
| `name`         | String | 사용자 이름                          |
| `email`        | String | 사용자 이메일                        |


### 2. Diary Collection
- 사용자가 작성하는 일기 데이터를 저장.
- 사용자가 로그인한 상태에서 일기를 작성하고, 일기에는 작성 날짜, 제목, 내용, 사진 파일이 포함됨.

| Field          | Type       | Description                           |
|----------------|------------|---------------------------------------|
| `_id`          | ObjectId   | MongoDB 고유 ID                      |
| `userId`       | ObjectId   | 작성자 ID (User 컬렉션의 `_id`와 참조 관계) |
| `date`         | Date       | 일기 작성 날짜                        |
| `title`        | String     | 일기 제목                             |
| `content`      | String     | 일기 내용                             |
| `photo`        | BinaryData | 사진 파일 (Base64 인코딩 또는 BinaryData 사용) |
| `createdAt`    | Date       | 일기 생성 날짜 (필요시 넣을 예정)|
| `updatedAt`    | Date       | 일기 수정 날짜 (필요시 넣을 예정)     |

### 3. Authentication
- Google 로그인 인증을 통해 사용자 정보를 받아오며, Google OAuth API에서 반환된 토큰을 `googleToken` 필드에 저장.
- 로그인 후 `googleToken`을 통해 User 컬렉션에서 사용자를 식별함.

## Notes
- MongoDB의 `_id` 필드는 기본적으로 고유 식별자로 사용됩니다.
- Diary 컬렉션의 `userId` 필드는 User 컬렉션의 `_id`와 매핑하여, 각 사용자가 작성한 일기 데이터를 쉽게 참조할 수 있게 합니다.
- 사진 파일은 용량을 줄이기 위해 적절히 압축하여 저장하거나 별도의 파일 스토리지와 연동할 수 있습니다.
